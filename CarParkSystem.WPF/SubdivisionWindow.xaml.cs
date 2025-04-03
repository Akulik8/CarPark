using CarParkSystem.App.DTOs;
using CarParkSystem.Domain.Models;
using CarParkSystem.WPF.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class SubdivisionWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private ObservableCollection<SubdivisionViewModel> _subdivisions = new();
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        private readonly Guid _userId;
        private readonly bool _isAdmin;

        public SubdivisionWindow(Guid currentUserID, bool IsAdmin)
        {
            InitializeComponent();
            _userId = currentUserID;
            _isAdmin = IsAdmin;

            if (IsAdmin)
            {
                AddRequestButton.Visibility = Visibility.Visible;
            }
            else
            {
                AddRequestButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadSubdivisions();
        }

        private async Task LoadSubdivisions()
        {
            try
            {
                List<SubdivisionDto> subdivisions;
                subdivisions = await _httpClient.GetFromJsonAsync<List<SubdivisionDto>>($"{baseUrl}/api/Subdivision");
                _subdivisions.Clear();

                foreach (var subdivision in subdivisions)
                {
                    _subdivisions.Add(new SubdivisionViewModel
                    {
                        SubdivisionID = subdivision.SubdivisionID,
                        Name = subdivision.Name,
                        Address = subdivision.Address,
                        PhoneNumber = subdivision.PhoneNumber,
                        Status = subdivision.Status
                    });
                }
                RequestGrid.ItemsSource = _subdivisions;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка при загрузке подразделений: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке подразделений: {ex.Message}");
            }
        }

        private void SearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = LoadFilteredSubdivisionsDataAsync();
        }
        private void SearchAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = LoadFilteredSubdivisionsDataAsync();
        }
        private void SearchPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = LoadFilteredSubdivisionsDataAsync();
        }
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LoadFilteredSubdivisionsDataAsync();
        }

        private async Task LoadFilteredSubdivisionsDataAsync()
        {
            try
            {
                string name = SearchName.Text?.Trim();
                string address = SearchAddress.Text?.Trim();
                string phoneNumber = SearchPhone.Text?.Trim();
                string status = (FilterStatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                var queryParams = new List<string>();

                if (!string.IsNullOrWhiteSpace(name)) queryParams.Add($"name={Uri.EscapeDataString(name)}");
                if (!string.IsNullOrWhiteSpace(address)) queryParams.Add($"address={Uri.EscapeDataString(address)}");
                if (!string.IsNullOrWhiteSpace(phoneNumber)) queryParams.Add($"phoneNumber={Uri.EscapeDataString(phoneNumber)}");
                if (!string.IsNullOrWhiteSpace(status) && status != "Все") queryParams.Add($"status={Uri.EscapeDataString(status)}");

                string query = string.Join("&", queryParams);
                var subdivisions = await _httpClient.GetFromJsonAsync<ObservableCollection<SubdivisionViewModel>>($"{baseUrl}/api/Subdivision/filter?{query}");


                _subdivisions.Clear();

                foreach (var subdivision in subdivisions)
                {
                    _subdivisions.Add(new SubdivisionViewModel
                    {
                        SubdivisionID = subdivision.SubdivisionID,
                        Name = subdivision.Name,
                        Address = subdivision.Address,
                        PhoneNumber = subdivision.PhoneNumber,
                        Status = subdivision.Status
                    });

                }

                RequestGrid.ItemsSource = _subdivisions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private async void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateSubdivisionWindow()
            {
                Owner = this
            };
            if (window.ShowDialog() == true)
            {
                await LoadSubdivisions();
            }
        }

        private async void RequestGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_isAdmin)
            {
                if (RequestGrid.SelectedItem is SubdivisionViewModel selected)
                {

                    var window = new UpdateSubdivisionWindow(new SubdivisionViewModel
                    {
                        SubdivisionID = selected.SubdivisionID,
                        Name = selected.Name,
                        Address = selected.Address,
                        PhoneNumber = selected.PhoneNumber,
                        Status = selected.Status
                    });
                    if (window.ShowDialog() == true)
                        await LoadSubdivisions();
                }

                //var deleteResult = await _httpClient.DeleteAsync($"{baseUrl}/api/subdivision/{selected.SubdivisionID}");
                //if (deleteResult.IsSuccessStatusCode)
                //    await LoadSubdivisions();

            }
        }
    }
}


