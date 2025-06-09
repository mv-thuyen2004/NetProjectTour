using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;
using System.Diagnostics;
using Microsoft.Maui.Controls;


namespace DoAn.ViewModels
{
    public partial class AddSessionViewModel : ObservableObject
    {
        [ObservableProperty] string startDate;
        [ObservableProperty] int totalSeats;
        [ObservableProperty] string message;

        private int _tourId;
        private readonly DatabaseServices _db;

        public AddSessionViewModel(DatabaseServices db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            Message = "Vui lòng nhập thông tin phiên mới.";
        }

        public void SetTourId(string tourId)
        {
            if (int.TryParse(tourId, out int id))
            {
                _tourId = id;
            }
            else
            {
                Message = "ID tour không hợp lệ.";
                Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task AddSession()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(StartDate) || TotalSeats <= 0)
                {
                    Message = "Vui lòng nhập đầy đủ và hợp lệ thông tin phiên.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                if (!DateTime.TryParseExact(StartDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime startDateParsed))
                {
                    Message = "Ngày bắt đầu không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                // Mặc định RemainingSeats = TotalSeats và Status = 0 (chưa bắt đầu)
                int rowsAffected = await _db.AddTourSession(_tourId, startDateParsed, TotalSeats, TotalSeats, 0);
                bool success = rowsAffected > 0;

                Message = success ? "Thêm phiên thành công!" : "Thêm phiên thất bại!";

                await Application.Current.MainPage.DisplayAlert(success ? "Success" : "Error", Message, "OK");

                if (success)
                {
                    Debug.WriteLine($"Session added successfully for TourId: {_tourId}");
                    MessagingCenter.Send<object>(this, "TourSessionAdded");
                    await Shell.Current.GoToAsync("//managetours");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddSession: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Thêm phiên thất bại do lỗi: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//managetours");
        }
    }
}