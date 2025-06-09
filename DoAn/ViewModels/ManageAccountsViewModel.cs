using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Models;
using DoAn.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DoAn.ViewModels
{
    public partial class ManageAccountsViewModel : ObservableObject
    {
        [ObservableProperty] ObservableCollection<Users> users;
        [ObservableProperty] string message;

        private readonly DatabaseServices _db;

        public ManageAccountsViewModel(DatabaseServices db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            Users = new ObservableCollection<Users>();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            try
            {
                var userList = await _db.GetAllUsers(); // Giả định có phương thức GetAllUsers
                Users.Clear();
                foreach (var user in userList)
                {
                    Users.Add(user);
                }
                Message = "Danh sách tài khoản đã được tải.";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadUsers: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi tải danh sách: {ex.Message}";
            }
        }
        [RelayCommand]
        private async Task Back()
        {
            System.Diagnostics.Debug.WriteLine("BackCommand executed");
            await Shell.Current.GoToAsync("///adminhome");
        }
        [RelayCommand]
        private async Task DeleteAccount(Users user)
        {
            try
            {
                if (user != null)
                {
                    // Kiểm tra nếu không phải là tài khoản Admin mặc định
                    if (user.Username == "admin" && user.Role == "Admin")
                    {
                        Message = "Không thể xóa tài khoản Admin mặc định!";
                        await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                        return;
                    }

                    bool confirm = await Application.Current.MainPage.DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa tài khoản {user.Username}?", "Có", "Không");
                    if (confirm)
                    {
                        int rowsAffected = await _db.DeleteUser(user.Id);
                        if (rowsAffected > 0)
                        {
                            Users.Remove(user);
                            Message = $"Đã xóa tài khoản {user.Username} thành công!";
                            await Application.Current.MainPage.DisplayAlert("Success", Message, "OK");
                        }
                        else
                        {
                            Message = $"Xóa tài khoản {user.Username} thất bại!";
                            await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteAccount: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi xóa tài khoản: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }

        }
    }
}