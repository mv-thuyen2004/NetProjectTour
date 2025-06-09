using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;

namespace DoAn.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty] string name;
        [ObservableProperty] string username;
        [ObservableProperty] string password;
        [ObservableProperty] string phonenumber;
        [ObservableProperty] string message;

        private readonly DatabaseServices _db;

        public RegisterViewModel(DatabaseServices db)
        {
            _db = db;
        }

        [RelayCommand]
        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Phonenumber))
            {
                Message = "Vui lòng nhập đầy đủ thông tin.";
                return;
            }

            if (await _db.IsUsernameExists(Username))
            {
                Message = "Tên đăng nhập đã được sử dụng. Vui lòng chọn tên khác.";
                return;
            }

            bool success = await _db.RegisterUser(Name, Username, Password, Phonenumber);
            Message = success ? "Đăng ký thành công!" : "Đăng ký thất bại!";
        }
    }
}