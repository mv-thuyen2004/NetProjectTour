using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Models;
using DoAn.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DoAn.ViewModels
{

    public partial class ManageToursViewModel : ObservableObject
    {
        [ObservableProperty] ObservableCollection<Tour> tours;
        [ObservableProperty] string message;

        private readonly DatabaseServices _db;

        public ManageToursViewModel(DatabaseServices db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            Tours = new ObservableCollection<Tour>();
            LoadToursAsync();
        }

        public async void OnAppearing()
        {
            await LoadToursAsync();
        }

        public async Task RefreshToursAsync()
        {
            await LoadToursAsync();
        }

        private async Task LoadToursAsync()
        {
            try
            {
                var tourList = await _db.GetTours();
                Tours.Clear();
                foreach (var tour in tourList)
                {
                    Tours.Add(tour);
                }
                Message = "Danh sách tour đã được tải.";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadToursAsync: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi tải danh sách: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task AddNewTour()
        {
            try
            {
                await Shell.Current.GoToAsync("//addtour");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddNewTour: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi chuyển đến trang thêm tour: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteTour(Tour tour)
        {
            try
            {
                if (tour != null)
                {
                    bool confirm = await Application.Current.MainPage.DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa tour {tour.TourName}?", "Có", "Không");
                    if (confirm)
                    {
                        int rowsAffected = await _db.DeleteTour(tour.TourId);
                        if (rowsAffected > 0)
                        {
                            Tours.Remove(tour);
                            Message = $"Đã xóa tour {tour.TourName} thành công!";
                            await Application.Current.MainPage.DisplayAlert("Success", Message, "OK");
                        }
                        else
                        {
                            Message = $"Xóa tour {tour.TourName} thất bại!";
                            await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteTour: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi xóa tour: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task EditTour(Tour tour)
        {
            try
            {
                if (tour != null)
                {
                    //await Application.Current.MainPage.DisplayAlert("Error", tour.TourId.ToString(), "OK");
                    await Shell.Current.GoToAsync($"//edittour?tourId={tour.TourId}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in EditTour: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi chuyển đến chỉnh sửa: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }
        [RelayCommand]
        private async Task AddSession(Tour tour)
        {
            try
            {
                if (tour == null)
                {
                    Debug.WriteLine("Tour is null in AddSession");
                    Message = "Không có tour để thêm phiên.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                Debug.WriteLine($"Navigating to add session for tour with ID: {tour.TourId}");
                await Shell.Current.GoToAsync($"//addsession?tourId={tour.TourId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddSession: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi chuyển đến trang thêm phiên: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            System.Diagnostics.Debug.WriteLine("BackCommand executed");
            await Shell.Current.GoToAsync("///adminhome");
        }
    }
}