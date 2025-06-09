using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Models;
using DoAn.Services;
using Microsoft.Maui.Storage;

namespace DoAn.ViewModels
{
    [QueryProperty(nameof(Tour), "tour")]
    public partial class TourDetailViewModel : ObservableObject
    {
        private readonly DatabaseServices _db;
        private int _userId;

        [ObservableProperty]
        private Tour tour;

        [ObservableProperty]
        private bool isFavorite;

        [ObservableProperty]
        private ObservableCollection<Review> reviews;

        [ObservableProperty]
        private string heartIcon = "heart.png";

        public TourDetailViewModel(DatabaseServices db)
        {
            _db = db;
            // Lấy UserId từ Preferences
            var userIdString = Preferences.Get("IdNguoiDung", "0");
            if (!int.TryParse(userIdString, out _userId) || _userId == 0)
            {
                System.Diagnostics.Debug.WriteLine("Error: Invalid or missing UserId in Preferences");
                _userId = 1; // Fallback cho test
            }
            System.Diagnostics.Debug.WriteLine($"TourDetailViewModel initialized, UserId: {_userId}");
            Reviews = new ObservableCollection<Review>(); // Khởi tạo mặc định
        }

        partial void OnTourChanged(Tour value)
        {
            if (value != null)
            {
                System.Diagnostics.Debug.WriteLine($"Tour changed: {value.TourName}, TourId: {value.TourId}");
                CheckFavoriteStatus();
                LoadReviewsAsync(); // Thêm dòng này để tải đánh giá khi Tour thay đổi
            }
        }

        private async void LoadReviewsAsync()
        {
            if (Tour != null && Tour.TourId > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Loading reviews for TourId: {Tour.TourId}");
                var tourSessions = await _db.GetAllTourSessionsByTourId(Tour.TourId).ConfigureAwait(false);
                if (tourSessions != null && tourSessions.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Found {tourSessions.Count} tour sessions for TourId: {Tour.TourId}");
                    Reviews.Clear();
                    foreach (var session in tourSessions)
                    {
                        System.Diagnostics.Debug.WriteLine($"Fetching reviews for TourSessionId: {session.Id}");
                        var sessionReviews = await _db.GetReviewsByToursId(session.Id);
                        System.Diagnostics.Debug.WriteLine($"Found {sessionReviews.Count} reviews for TourSessionId: {session.Id}");
                        foreach (var review in sessionReviews)
                        {
                            Reviews.Add(review);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Total reviews loaded: {Reviews.Count}");
                }
                else
                {
                    Reviews = new ObservableCollection<Review>();
                    System.Diagnostics.Debug.WriteLine("No tour sessions found for this tour.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Tour is null or TourId is invalid.");
            }
        }

        private async void CheckFavoriteStatus()
        {
            try
            {
                IsFavorite = await _db.IsTourFavorite(_userId, Tour.TourId);
                HeartIcon = IsFavorite ? "heartred.png" : "heart.png";
                System.Diagnostics.Debug.WriteLine($"Checked favorite status: UserId={_userId}, TourId={Tour.TourId}, IsFavorite={IsFavorite}, HeartIcon={HeartIcon}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CheckFavoriteStatus error: {ex.Message}, StackTrace: {ex.StackTrace}");
                await Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể kiểm tra trạng thái yêu thích: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            System.Diagnostics.Debug.WriteLine("BackCommand executed");
            await Shell.Current.GoToAsync("///home");
        }

        [RelayCommand]
        private async Task BookNow(Tour tour)
        {
            try
            {
                if (tour == null)
                {
                    System.Diagnostics.Debug.WriteLine("Tour is null");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Navigating to BookPage for tour: {tour.TourName}");
                var navigationParameter = new Dictionary<string, object>
                {
                    { "tour", tour },
                    { "userId", _userId }
                };
                await Shell.Current.GoToAsync("//bookpage", true, navigationParameter);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NavigateToBookPage error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task ToggleFavorite()
        {
            try
            {
                if (_userId == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Lỗi", "Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.", "OK");
                    return;
                }

                IsFavorite = await _db.ToggleFavoriteTour(_userId, Tour.TourId);
                HeartIcon = IsFavorite ? "heartred.png" : "heart.png";
                System.Diagnostics.Debug.WriteLine($"ToggleFavorite: UserId={_userId}, TourId={Tour.TourId}, IsFavorite={IsFavorite}, HeartIcon={HeartIcon}");
                await Application.Current.MainPage.DisplayAlert("Thông báo", IsFavorite ? "Đã thêm tour vào danh sách yêu thích!" : "Đã xóa tour khỏi danh sách yêu thích!", "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ToggleFavorite error: {ex.Message}, StackTrace: {ex.StackTrace}");
                await Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể cập nhật danh sách yêu thích: {ex.Message}", "OK");
            }
        }
    }
}