using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarParkSystem.App.DTOs;

namespace CarParkSystem.WPF;


public partial class LoginWindow : Window
{
    private readonly HttpClient _httpClient = new HttpClient();

    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameBox.Text;
        var password = PasswordBox.Visibility == Visibility.Visible ? PasswordBox.Password : VisiblePasswordBox.Text;


        var loginDto = new
        {
            Username = username,
            Password = password
        };

        var json = JsonConvert.SerializeObject(loginDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        try
        {
            var response = await _httpClient.PostAsync($"{baseUrl}/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDto>(responseString);

                if (user.Role == "manager" || user.Role == "admin" )
                {
                    var managerWindow = new ManagerWindow(user.UserID);
                    managerWindow.Show();
                }
                //else
                //{
                //    var userWindow = new UserWindow();
                //    userWindow.Show();
                //}

                this.Close(); // закрываем окно авторизации
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Ошибка авторизации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка сети: " + ex.Message);
        }
    }


    private void TogglePasswordVisibilityBtn_Click(object sender, RoutedEventArgs e)
    {
        if (PasswordBox.Visibility == Visibility.Visible)
        {
            VisiblePasswordBox.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            VisiblePasswordBox.Visibility = Visibility.Visible;
            EyeIcon.Text = "🙈";
        }
        else
        {
            PasswordBox.Password = VisiblePasswordBox.Text;
            VisiblePasswordBox.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
            EyeIcon.Text = "👁";
        }
    }

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            LoginButton_Click(sender, e);
            e.Handled = true;
        }
    }
}
