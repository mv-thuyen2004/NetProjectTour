using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System.Diagnostics;
using System.Xml.Linq;

namespace DoAn.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string phonenumber;

        [ObservableProperty]
        private int userId;

        public ProfileViewModel()
        {
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            try
            {
                UserId = int.Parse(Preferences.Get("IdNguoiDung", "0"));
                Name = Preferences.Get("TenNguoiDung", "Không có dữ liệu");
                Username = Preferences.Get("UsernameNguoiDung", "Không có dữ liệu");
                Phonenumber = Preferences.Get("Phonenumber", "Không có dữ liệu");

                if (UserId == 0)
                {
                    Debug.WriteLine("Error: Invalid or missing UserId in Preferences");
                }
                else
                {
                    Debug.WriteLine($"Loaded user info: UserId={UserId}, Name={Name}, Username={Username}, Phonenumber={Phonenumber}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadUserInfo error: {ex.Message}, StackTrace: {ex.StackTrace}");
                Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể tải thông tin người dùng: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task Logout()
        {
            try
            {
                Debug.WriteLine($"Logging out UserId: {UserId}");
                Preferences.Remove("IdNguoiDung");
                Preferences.Remove("TenNguoiDung");
                Preferences.Remove("UsernameNguoiDung");
                Preferences.Remove("UserRole");
                Preferences.Remove("Phonenumber");

                await Shell.Current.GoToAsync("///login");
                Debug.WriteLine("Logged out successfully, navigated to LoginPage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Logout error: {ex.Message}, StackTrace: {ex.StackTrace}");
                await Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể đăng xuất: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        private async Task Tab1(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab1: Staying on HomePage");
            await Shell.Current.GoToAsync("//home");
        }

        [RelayCommand]
        private async Task Tab2(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab2: Navigating to FavoritesPage");
            await Shell.Current.GoToAsync("//favorite");
        }

        [RelayCommand]
        private async Task Tab3(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab3: Navigating to BookingPage");
            await Shell.Current.GoToAsync("//booking");
        }

        [RelayCommand]
        private async Task Tab4(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab4: Navigating to ProfilePage");
            
        }
    }
}