using CarParkSystem.App.DTOs;
using CarParkSystem.WPF.ViewModel;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarParkSystem.WPF
{
    public partial class ManagerWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient(); // адрес API
        private ObservableCollection<BidViewModel> _bids { get; set; } = new();
        string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        private readonly Guid _userID;
        private bool _isAdmin = false;

        public ManagerWindow(Guid currentUserId)
        {
            InitializeComponent();
            _userID = currentUserId;
            //StartDatePicker.SelectedDate = DateTime.Today;
            //EndDatePicker.SelectedDate = DateTime.Today;
        }

        //private readonly System.Timers.Timer _refreshTimer = new System.Timers.Timer(10000); // каждые 10 сек

        //private void InitAutoRefresh()
        //{
        //    _refreshTimer.Elapsed += async (s, e) =>
        //    {
        //        await Dispatcher.InvokeAsync(async () =>
        //        {
        //            await LoadBidDataAsync();
        //        });
        //    };
        //    _refreshTimer.AutoReset = true;
        //    _refreshTimer.Start();
        //}

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBidDataAsync();
            //InitAutoRefresh();
        }

        private async Task LoadBidDataAsync()
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<UserDto>($"{baseUrl}/api/User/{_userID}");
                UserName.Text = user.Surname + " " + user.Name;
                List<BidDto> bids;

                if (user.Role == "admin")
                {
                    _isAdmin = true;
                    bids = await _httpClient.GetFromJsonAsync<List<BidDto>>($"{baseUrl}/api/Bid");
                }
                else
                {
                    _isAdmin = false;
                    bids = await _httpClient.GetFromJsonAsync<List<BidDto>>($"{baseUrl}/api/Bid?userId={_userID}");
                }
                // Получаем подразделения
                var subdivisions = await _httpClient.GetFromJsonAsync<List<SubdivisionDto>>($"{baseUrl}/api/Subdivision");

                var subdivisionMap = subdivisions.ToDictionary(s => s.SubdivisionID, s => s.Name);

                _bids.Clear();

                foreach (var bid in bids)
                {
                    _bids.Add(new BidViewModel
                    {
                        BidID = bid.BidID,
                        DeliveryDate = bid.DeliveryDate.ToLocalTime(),
                        DoDate = bid.DoDate.ToLocalTime(),
                        Cargo = bid.Cargo,
                        Weight = bid.Weight,
                        Volume = bid.Volume,
                        From = bid.From,
                        To = bid.To,
                        Note = bid.Note,
                        Status = bid.Status,
                      //  SubdivisionID = bid.SubdivisionID,
                        SubdivisionName = subdivisionMap.ContainsKey(bid.SubdivisionID) ? subdivisionMap[bid.SubdivisionID] : "Неизвестно"
                    });
                }

                RequestGrid.ItemsSource = _bids;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заявок: {ex.Message}");
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new UpdatePasswordWindow(_userID)
            {
                Owner = this
            };
            addWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private async void RequestGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (RequestGrid.SelectedItem is BidViewModel selectedBid)
            {
                // проверка статуса
                if (!string.Equals(selectedBid.Status, "На рассмотрении", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Редактирование доступно только для заявок со статусом 'На рассмотрении'", "Ограничение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var editWindow = new EditRequestWindow(selectedBid, _userID)
                {
                    Owner = this
                };

                if (editWindow.ShowDialog() == true)
                {
                    if (editWindow.IsDeleted)
                    {
                        _bids.Remove(selectedBid);
                    }

                    await LoadBidDataAsync();
                }
            }
        }

        private async void OpenAddDialog_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddRequestWindow(_userID)
            {
                Owner = this
            };

            if (addWindow.ShowDialog() == true)
            {
                await LoadBidDataAsync();
            }
        }

        private async void OpenSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new SubdivisionWindow(_userID, _isAdmin)
            {
                Owner = this
            };
            addWindow.ShowDialog();
        }

        private void SearchRequest_Click(object sender, RoutedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }

        private async Task LoadFilteredBidDataAsync()
        {
            try
            {
                string cargo = SearchCargo.Text?.Trim();
                string from = SearchFrom.Text?.Trim();
                string to = SearchTo.Text?.Trim();
                string status = (FilterPeriodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                DateTime? start = StartDatePicker.SelectedDate;
                DateTime? end = EndDatePicker.SelectedDate;

                var user = await _httpClient.GetFromJsonAsync<UserDto>($"{baseUrl}/api/User/{_userID}");

                var queryParams = new List<string>(); 
                if (user.Role == "manager")
                    queryParams.Add($"userId={_userID}");

                if (!string.IsNullOrWhiteSpace(cargo)) queryParams.Add($"cargo={Uri.EscapeDataString(cargo)}");
                if (!string.IsNullOrWhiteSpace(from)) queryParams.Add($"from={Uri.EscapeDataString(from)}");
                if (!string.IsNullOrWhiteSpace(to)) queryParams.Add($"to={Uri.EscapeDataString(to)}");
                if (!string.IsNullOrWhiteSpace(status) && status != "Все") queryParams.Add($"status={Uri.EscapeDataString(status)}");
                if (start.HasValue) queryParams.Add($"startDate={start.Value:yyyy-MM-dd}");
                if (end.HasValue) queryParams.Add($"endDate={end.Value:yyyy-MM-dd}");

                string query = string.Join("&", queryParams);
                var bids = await _httpClient.GetFromJsonAsync<ObservableCollection<BidViewModel>>($"{baseUrl}/api/Bid/filter?{query}");

                var subdivisions = await _httpClient.GetFromJsonAsync<List<SubdivisionDto>>($"{baseUrl}/api/Subdivision");

                var subdivisionMap = subdivisions.ToDictionary(s => s.SubdivisionID, s => s.Name);

                _bids.Clear();

                foreach (var bid in bids)
                {
                    _bids.Add(new BidViewModel
                    {
                        BidID = bid.BidID,
                        DeliveryDate = bid.DeliveryDate.ToLocalTime(),
                        DoDate = bid.DoDate.ToLocalTime(),
                        Cargo = bid.Cargo,
                        Weight = bid.Weight,
                        Volume = bid.Volume,
                        From = bid.From,
                        To = bid.To,
                        Note = bid.Note,
                        Status = bid.Status,
                        //  SubdivisionID = bid.SubdivisionID,
                        SubdivisionName = subdivisionMap.ContainsKey(bid.SubdivisionID) ? subdivisionMap[bid.SubdivisionID] : "Неизвестно"
                    });
                }

                RequestGrid.ItemsSource = _bids;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }

        // Скрытие панели при клике вне
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (UserPanel.Visibility == Visibility.Visible &&
            //    !UserPanel.IsMouseOver &&
            //    !UserProfileButton.IsMouseOver)
            //{
            //    UserPanel.Visibility = Visibility.Collapsed;
            //}
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Escape && UserPanel.Visibility == Visibility.Visible)
            //{
            //    UserPanel.Visibility = Visibility.Collapsed;
            //}
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Escape && UserPanel.Visibility == Visibility.Visible)
            //{
            //    UserPanel.Visibility = Visibility.Collapsed;
            //}
        }

        private void SearchFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }

        private void SearchTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ = LoadFilteredBidDataAsync();
        }
    }
}
