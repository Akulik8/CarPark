using CarParkSystem.App.DTOs;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarParkSystem.WPF
{
    public partial class CreateSubdivisionWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public CreateSubdivisionWindow()
        {
            InitializeComponent();
        }


        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                string.IsNullOrWhiteSpace(AddressBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var dto = new CreateSubdivisionDto
                {
                    Name = NameBox.Text,
                    Address = AddressBox.Text,
                    PhoneNumber = PhoneNumberBox.Text,
                    Status = "Действующее"
                };

                var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/api/Subdivision", dto);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Подразделение добавлено.");
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
