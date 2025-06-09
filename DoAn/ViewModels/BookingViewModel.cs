using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;
using DoAn.Models;
using DoAn.Views;

namespace DoAn.ViewModels
{
    public partial class BookingViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<BookingDetail> bookings;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private int selectedRating; // Đánh giá tạm thời
        [ObservableProperty]
        private string reviewComment; // Bình luận tạm thời
        [ObservableProperty]
        private BookingDetail selectedBooking; // Booking được chọn để đánh giá

        [ObservableProperty] // Thêm thuộc tính này
        private List<int> ratingOptions; // Danh sách lựa chọn rating (1-5)

        private readonly DatabaseServices _db;
        private readonly int _userId;

        public BookingViewModel(DatabaseServices db)
        {
            _db = db;
            Bookings = new ObservableCollection<BookingDetail>();
            SelectedRating = 0; // Mặc định không chọn
            ErrorMessage = string.Empty;
            RatingOptions = new List<int> { 1, 2, 3, 4, 5 }; // Khởi tạo danh sách rating trong constructor

            // Lấy userId từ Preferences (giả sử đã lưu khi người dùng đăng nhập)
            var userIdString = Preferences.Get("IdNguoiDung", "0");
            if (!int.TryParse(userIdString, out _userId) || _userId == 0)
            {
                _userId = 1; // Giá trị mặc định cho testing
            }

            LoadBookingsAsync();
        }

        public async void LoadBookingsAsync()
        {
            try
            {
                var userBookings = await _db.GetBookingsByUserId(_userId);
                Bookings.Clear();

                foreach (var booking in userBookings)
                {
                    var tourSession = await _db.GetTourSessionById(booking.TourSessionId);
                    var tour = tourSession != null ? await _db.GetTourById(tourSession.TourId) : null;

                    // Hiển thị booking ngay cả khi tour hoặc tourSession không tồn tại
                    var bookingDetail = new BookingDetail
                    {
                        BookingId = booking.Id,
                        TourName = tour?.TourName ?? "Tour không tồn tại",
                        TourSessionId = tourSession?.Id ?? booking.TourSessionId,
                        TourSessionDate = tourSession?.StartDate ?? DateTime.MinValue,
                        NumberOfPeople = booking.NumberOfPeople,
                        Status = tourSession != null ? (tourSession.Status == 0 ? "Đã đặt" : "Đã hoàn thành") : "Không xác định",
                        CanReview = booking.CanReview == 1,
                        TotalPrice = tour != null ? booking.NumberOfPeople * tour.Price : 0
                    };
                    Bookings.Add(bookingDetail);
                }

                ErrorMessage = Bookings.Count == 0 ? "Bạn chưa có đơn đặt tour nào." : string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Không thể tải danh sách đặt tour: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task SubmitReview()
        {
            try
            {
                if (SelectedBooking == null || SelectedRating < 1 || SelectedRating > 5)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Vui lòng chọn booking và đánh giá từ 1 đến 5 sao.", "OK");
                    return;
                }

                var review = new Review
                {
                    UserId = _userId,
                    TourSessionId = SelectedBooking.TourSessionId,
                    Rating = SelectedRating,
                    Comment = string.IsNullOrWhiteSpace(ReviewComment) ? null : ReviewComment.Trim()
                };

                int rowsAffected = await _db.AddReview(review);

                if (rowsAffected > 0)
                {
                    // Cập nhật CanReview trong cơ sở dữ liệu thành 0
                    var bookingToUpdate = new Models.Booking { Id = SelectedBooking.BookingId, CanReview = 0 };
                    int updateRows = await _db.UpdateBooking(bookingToUpdate);

                    if (updateRows > 0)
                    {
                        // Cập nhật SelectedBooking trong bộ nhớ
                        SelectedBooking.CanReview = false;

                        // Lấy TourId từ TourSession để cập nhật AvgRate
                        var tourSession = await _db.GetTourSessionById(SelectedBooking.TourSessionId);
                        if (tourSession != null)
                        {
                            await _db.UpdateAvgRate(tourSession.TourId);
                        }
                        else
                        {
                        }

                        // Làm mới danh sách bookings
                        LoadBookingsAsync();
                    }
                    else
                    {
                    }

                    await Application.Current.MainPage.DisplayAlert("Success", "Đánh giá đã được gửi thành công!", "OK");

                    SelectedRating = 0;
                    ReviewComment = string.Empty;
                    SelectedBooking = null;
                    await Shell.Current.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Gửi đánh giá thất bại.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Lỗi khi gửi đánh giá: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task OpenReviewPopup(BookingDetail booking)
        {
            if (booking == null || !booking.CanReview)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Không thể đánh giá booking này.", "OK");
                return;
            }

            SelectedBooking = booking;
            SelectedRating = 0; // Reset rating
            ReviewComment = string.Empty; // Reset comment

            try
            {
                await Shell.Current.Navigation.PushModalAsync(new ReviewPopupPage(this)); // Sử dụng Shell navigation
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Lỗi khi mở popup: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            try
            {
                await Shell.Current.Navigation.PopModalAsync(); // Đóng popup
            }
            catch (Exception ex)
            {
            }
            SelectedRating = 0; // Reset rating
            ReviewComment = string.Empty; // Reset comment
            SelectedBooking = null; // Reset booking
        }

        [RelayCommand]
        private async Task Tab1(ContentPage page)
        {
            // Điều hướng đến trang Home (đã định nghĩa route là "//HomePage")
            await Shell.Current.GoToAsync("///home");
        }

        [RelayCommand]
        private async Task Tab2(ContentPage page)
        {
            // Điều hướng đến trang Yêu thích (giả sử route là "FavoritesPage")
            await Shell.Current.GoToAsync("///favorite");
        }

        [RelayCommand]
        private async Task Tab3(ContentPage page)
        {
            // Điều hướng đến trang Giỏ hàng (giả sử route là "CartPage")
        }

        [RelayCommand]
        private async Task Tab4(ContentPage page)
        {
            // Điều hướng đến trang Hồ sơ (giả sử route là "ProfilePage")
            await Shell.Current.GoToAsync("///profile");
        }
    }

    // Class để hiển thị thông tin chi tiết của booking
    public class BookingDetail
    {
        public int BookingId { get; set; }
        public string TourName { get; set; }
        public int TourSessionId { get; set; }
        public DateTime TourSessionDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; set; }
        public bool CanReview { get; set; }
        public decimal TotalPrice { get; set; }
    }
}