using CarParkSystem.App.DTOs;
using CarParkSystem.Domain.Models;
using CarParkSystem.WPF.ViewModel;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace CarParkSystem.WPF
{
    /// <summary>
    /// Логика взаимодействия для EditRequestWindow.xaml
    /// </summary>
    public partial class EditRequestWindow : Window
    {
        public bool IsDeleted { get; private set; } = false;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Guid _bidId;
        private readonly Guid _userId;
        private readonly Guid _subdivisionId;
        private Dictionary<string, Guid> _subdivisionMap = new();
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public EditRequestWindow(BidViewModel bid, Guid userId)
        {
            InitializeComponent();
            _bidId = bid.BidID;
            _userId = userId;
            _subdivisionId = bid.SubdivisionID;
            //CurrentRequest = request;

            // заполнение данных заявки
            DoDatePicker.SelectedDate = bid.DoDate.ToLocalTime();
            CargoBox.Text = bid.Cargo;
            WeightBox.Text = bid.Weight.ToString();
            VolumeBox.Text = bid.Volume.ToString();
            FromBox.Text = bid.From;
            ToBox.Text = bid.To;
            NoteBox.Text = bid.Note;
        }

        private async void EditRequestWindow_Loaded(object sender, RoutedEventArgs e)
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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация и сохранение данных
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

                var dto = new BidDto
                {
                    BidID = _bidId,
                    DeliveryDate = DateTime.Now.ToUniversalTime(),
                    DoDate = (DoDatePicker.SelectedDate ?? DateTime.Now).ToUniversalTime(),
                    Cargo = CargoBox.Text,
                    Weight = double.TryParse(WeightBox.Text, out var w) ? w : 0,
                    Volume = double.TryParse(VolumeBox.Text, out var v) ? v : 0,
                    From = FromBox.Text,
                    To = ToBox.Text,
                    Note = NoteBox.Text,
                    SubdivisionID = _subdivisionMap[selectedSubdivision],
                    UserID = _userId,
                    Status = "На рассмотрении"
                };

                var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/api/Bid/{_bidId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Заявка обновлена.");
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при обновлении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Вы уверены, что хотите удалить эту заявку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm != MessageBoxResult.Yes)
                return;
            try
            {
                var response = await _httpClient.DeleteAsync($"{baseUrl}/api/Bid/{_bidId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Заявка удалена.");
                    IsDeleted = true;
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при удалении: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }
    }
}
