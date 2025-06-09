using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;
using DoAn.Models;

namespace DoAn.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private string username;
        [ObservableProperty] private string password;
        [ObservableProperty] private string message;

        private readonly DatabaseServices _db;

        public LoginViewModel(DatabaseServices db)
        {
            _db = db;
        }

        [RelayCommand]
        private async Task Login()
        {
            System.Diagnostics.Debug.WriteLine($"LoginCommand executed. Username: {Username}, Password: {Password}");
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return;
            }

            try
            {
                var user = await _db.GetUser(Username, Password);
                if (user != null)
                {
                    // Lưu thông tin người dùng vào Preferences
                    Preferences.Set("TenNguoiDung", user.Name);
                    Preferences.Set("UsernameNguoiDung", user.Username);
                    Preferences.Set("IdNguoiDung", user.Id.ToString());
                    Preferences.Set("UserRole", user.Role);
                    Preferences.Set("Phonenumber", user.Phonenumber);

                    // Điều hướng tới trang Home
                    // TODO: Phân biệt giao diện hoặc trang cho Admin và User nếu cần
                    //await Shell.Current.GoToAsync("//home");
                    // Đăng nhập thành công
                    if (user.Role == "Admin")
                    {
                        await Shell.Current.GoToAsync("//adminhome"); // Điều hướng đến AdminHomePage
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("//home"); // Điều hướng đến HomePage cho user thường
                    }

                }
                else
                {
                    Message = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }

            catch (Exception ex)
            {
                Message = "Đã xảy ra lỗi khi đăng nhập!";
                System.Diagnostics.Debug.WriteLine($"Login error: {ex}");
            }
        }
    }
}