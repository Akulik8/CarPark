using CarParkSystem.App.DTOs;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace CarParkSystem.WPF
{
    public partial class AddRequestWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private Dictionary<string, Guid> _subdivisionMap = new();
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        private readonly Guid _userId;

        public AddRequestWindow(Guid userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += AddRequestWindow_Loaded;
        }

        private async void AddRequestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var subdivisions = await _httpClient.GetFromJsonAsync<List<SubdivisionDto>>($"{baseUrl}/api/Subdivision");
                _subdivisionMap.Clear();

                foreach (var subdivision in subdivisions)
                {
                    if (subdivision.Status == "Действующее")
                    {
                        _subdivisionMap[subdivision.Name] = subdivision.SubdivisionID;
                        SubdivisionNameBox.Items.Add(subdivision.Name);
                    }
                }

                //if (SubdivisionNameBox.Items.Count > 0)
                //    SubdivisionNameBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки подразделений: {ex.Message}");
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(CargoBox.Text) ||
                string.IsNullOrWhiteSpace(WeightBox.Text) ||
                string.IsNullOrWhiteSpace(VolumeBox.Text) ||
                string.IsNullOrWhiteSpace(FromBox.Text) ||
                string.IsNullOrWhiteSpace(ToBox.Text) ||
                !DoDatePicker.SelectedDate.HasValue ||
                SubdivisionNameBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите дату выполнения.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DoDatePicker.SelectedDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Дата выполнения не может быть раньше сегодняшней.", "Недопустимая дата", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var selectedSubdivision = SubdivisionNameBox.SelectedItem as string;
                if (selectedSubdivision == null || !_subdivisionMap.ContainsKey(selectedSubdivision))
                {
                    MessageBox.Show("Выберите подразделение");
                    return;
                }

                var dto = new CreateBidDto
                {
                    DeliveryDate = DateTime.Now.ToUniversalTime(),
                    DoDate = (DoDatePicker.SelectedDate ?? DateTime.Now).ToUniversalTime(),
                    Cargo = CargoBox.Text,
                    Weight = double.TryParse(WeightBox.Text, out var w) ? w : 0,
                    Volume = double.TryParse(VolumeBox.Text, out var v) ? v : 0,
                    From = FromBox.Text,
                    To = ToBox.Text,
                    Note = NoteBox.Text,
                    SubdivisionID = _subdivisionMap[selectedSubdivision],
                    UserID = _userId
                };

                var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/api/Bid", dto);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Заявка добавлена.");
                    DialogResult = true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка добавления: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
