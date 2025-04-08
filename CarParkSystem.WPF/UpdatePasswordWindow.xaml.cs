using CarParkSystem.App.DTOs;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarParkSystem.WPF
{
    /// <summary>
    /// Логика взаимодействия для UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Guid _userId;
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public UpdatePasswordWindow(Guid userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newPassword1.Password) || string.IsNullOrWhiteSpace(newPassword2.Password))
            {
                MessageBox.Show("Введите новый пароль дважды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (newPassword1.Password != newPassword2.Password)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var user = await _httpClient.GetFromJsonAsync<UserDto>($"{baseUrl}/api/User/{_userId}");

                var update = new CreateUserDto
                {
                    Password = newPassword1.Password,
                    Username = user.Username,
                    Name = user.Name,
                    Surname = user.Surname,
                    Role = user.Role,
                    Status = user.Status
                };

                var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/api/user/{_userId}", update);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Пароль успешно изменён.");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
