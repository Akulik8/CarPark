using CarParkSystem.App.DTOs;
using CarParkSystem.Domain.Models;
using CarParkSystem.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarParkSystem.WPF
{
    /// <summary>
    /// Логика взаимодействия для UpdateSubdivisionWindow.xaml
    /// </summary>
    public partial class UpdateSubdivisionWindow : Window
    {
        public bool IsDeleted { get; private set; } = false;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Guid _subdivisionId;
        private readonly string _subdivisionStatus;
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public UpdateSubdivisionWindow(SubdivisionViewModel subdivision)
        {
            InitializeComponent();
            _subdivisionId = subdivision.SubdivisionID;
            NameBox.Text = subdivision.Name;
            AddressBox.Text = subdivision.Address;
            PhoneNumberBox.Text = subdivision.PhoneNumber;
            SubdivisionStatusBox.ItemsSource = new List<string> { "Действующее", "Упразднено" };
            SubdivisionStatusBox.SelectedItem = subdivision.Status;
            _subdivisionStatus = subdivision.Status;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedSubdivisionStatus = SubdivisionStatusBox.SelectedItem as string;
                if (selectedSubdivisionStatus == null)
                {
                    MessageBox.Show("Выберите статус");
                    return;
                }

                if (_subdivisionStatus!=selectedSubdivisionStatus)
                {
                    // Проверка наличия активных заявок перед редактированием
                    var resp = await _httpClient.GetAsync($"{baseUrl}/api/bid/by-subdivision-chek-status/{_subdivisionId}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var json = await resp.Content.ReadAsStringAsync();
                        var bids = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BidDto>>(json);

                        bool hasActive = bids.Any(b => b.Status == "На рассмотрении" || b.Status == "В работе");
                        if (hasActive)
                        {
                            MessageBox.Show("Нельзя редактировать подразделение: есть активные заявки.", "Ограничение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при проверке активных заявок.");
                        return;
                    }
                }

                var dto = new SubdivisionDto
                {
                    SubdivisionID = _subdivisionId,
                    Name = NameBox.Text,
                    Address = AddressBox.Text,
                    PhoneNumber = PhoneNumberBox.Text,
                    Status = selectedSubdivisionStatus
                };

                var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/api/Subdivision/{_subdivisionId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Подразделение обновлено.");
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
            var confirm = MessageBox.Show("Вы уверены, что хотите удалить это подразделение?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm != MessageBoxResult.Yes)
                return;
            try
            {
                    // Проверка наличия заявок перед удалением
                    var resp = await _httpClient.GetAsync($"{baseUrl}/api/bid/by-subdivision/{_subdivisionId}");
                    if (resp.IsSuccessStatusCode)
                    {
                        var json = await resp.Content.ReadAsStringAsync();
                        var bids = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BidDto>>(json);

                        bool chek = bids.Any(b => b.Status == "На рассмотрении" || b.Status == "В работе" || b.Status =="Выполнена");
                        if (chek)
                        {
                            MessageBox.Show("Нельзя удалить подразделение: у него были заявки. \nВы можете поменять статус  на \"Упразднено\"", "Ограничение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при проверке заявок.");
                        return;
                    }
                var response = await _httpClient.DeleteAsync($"{baseUrl}/api/Subdivision/{_subdivisionId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("подразделение удалено.");
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
