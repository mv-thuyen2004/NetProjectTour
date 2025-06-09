using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Models;
using DoAn.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace DoAn.ViewModels
{
    public class TourSessionGroup : List<TourSessions>
    {
        public Tour Tour { get; set; }

        public TourSessionGroup(Tour tour, List<TourSessions> sessions) : base(sessions)
        {
            Tour = tour;
        }
    }

    public partial class ManageSessionsViewModel : ObservableObject
    {
        [ObservableProperty] ObservableCollection<TourSessionGroup> sessionGroups;
        [ObservableProperty] string message;

        private readonly DatabaseServices _db;

        public ManageSessionsViewModel(DatabaseServices db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            SessionGroups = new ObservableCollection<TourSessionGroup>();
            // Đăng ký lắng nghe thông báo từ MessagingCenter
            MessagingCenter.Subscribe<object>(this, "TourSessionAdded", (sender) =>
            {
                LoadSessions(); // Làm mới danh sách khi nhận thông báo
            });
            LoadSessions();
        }

        public async void LoadSessions()
        {
            try
            {
                var toursWithSessions = await _db.GetToursWithSessionsStatusZero();
                SessionGroups.Clear();
                Debug.WriteLine($"Number of tour groups: {toursWithSessions.Count}");

                foreach (var kvp in toursWithSessions)
                {
                    var group = new TourSessionGroup(kvp.Key, kvp.Value);
                    SessionGroups.Add(group);
                    Debug.WriteLine($"Tour: {kvp.Key.TourName}, Number of sessions: {kvp.Value.Count}");
                }

                Message = SessionGroups.Any() ? "Danh sách tất cả các phiên tour (chưa bắt đầu) đã được tải." : "Không có phiên tour nào (chưa bắt đầu).";
                //await Application.Current.MainPage.DisplayAlert("Info", Message, "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadSessions: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi tải danh sách phiên: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task ToggleStatus(TourSessions session)
        {
            try
            {
                if (session == null)
                {
                    Message = "Không có phiên để cập nhật trạng thái.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                // Lưu ID của phiên để tìm kiếm sau này
                int sessionId = session.Id;

                // Chuyển đổi trạng thái: 0 -> 1 hoặc 1 -> 0
                session.Status = session.Status == 0 ? 1 : 0;
                int rowsAffected = await _db.UpdateTourSession(session);

                bool success = rowsAffected > 0;
                Message = success ? $"Cập nhật trạng thái phiên ngày {session.StartDate:dd/MM/yyyy} thành công!" : "Cập nhật trạng thái thất bại!";
                if (success && session.Status == 1)
                {
                    // Cập nhật TotalBooked cho tour
                    await _db.UpdateTotalBooked(session.TourId);
                    var bookings = await _db.GetBookingsByTourSessionId(sessionId);
                    foreach (var booking in bookings)
                    {
                        booking.CanReview = 1; // Cho phép đánh giá
                        await _db.UpdateBooking(booking);
                        System.Diagnostics.Debug.WriteLine($"Updated BookingId {booking.Id} to CanReview = 1");
                    }
                }
                await Application.Current.MainPage.DisplayAlert(success ? "Success" : "Error", Message, "OK");

                if (success && session.Status == 1) // Nếu chuyển sang "Đã hoàn thành", làm mới danh sách
                {
                    LoadSessions(); // Làm mới toàn bộ danh sách từ cơ sở dữ liệu
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ToggleStatus: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Cập nhật trạng thái thất bại do lỗi: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteSession(TourSessions session)
        {
            try
            {
                if (session != null)
                {
                    bool confirm = await Application.Current.MainPage.DisplayAlert("Xác nhận", $"Bạn có chắc muốn xóa phiên ngày {session.StartDate:dd/MM/yyyy}?", "Có", "Không");
                    if (confirm)
                    {
                        int rowsAffected = await _db.DeleteTourSession(session.Id);
                        if (rowsAffected > 0)
                        {
                            Message = $"Đã xóa phiên ngày {session.StartDate:dd/MM/yyyy} thành công!";
                            await Application.Current.MainPage.DisplayAlert("Success", Message, "OK");

                            // Làm mới toàn bộ danh sách từ cơ sở dữ liệu
                            LoadSessions();
                        }
                        else
                        {
                            Message = $"Xóa phiên ngày {session.StartDate:dd/MM/yyyy} thất bại!";
                            await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteSession: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi xóa phiên: {ex.Message}";
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