using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Models;
using DoAn.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DoAn.ViewModels
{
    [QueryProperty(nameof(Tour), "tour")]
    [QueryProperty(nameof(UserId), "userId")]
    public partial class BookViewModel : ObservableObject
    {
        [ObservableProperty] private ObservableCollection<TourSessions> tourSessions;
        [ObservableProperty] private TourSessions selectedTourSession;
        [ObservableProperty] private int numberOfTickets;
        [ObservableProperty] private decimal totalPrice;
        [ObservableProperty] private string message;
        [ObservableProperty] private int tourId;
        [ObservableProperty] private int userId;
        [ObservableProperty] private Tour tour;


        private readonly DatabaseServices _db;
        private decimal _tourPrice;
        

        public BookViewModel(DatabaseServices db)
        {
            _db = db;
            TourSessions = new ObservableCollection<TourSessions>();
            NumberOfTickets = 0;
            TotalPrice = 0;
            Message = string.Empty;
            
        }
        public async Task InitializeAsync()
        {
            try
            {

                Debug.WriteLine("da load");
                

                var sessions = await _db.GetTourSessionsByTourId(Tour.TourId);
                TourSessions.Clear();
                foreach (var session in sessions)
                {
                    TourSessions.Add(session);
                    Debug.WriteLine("da chay den day");
                }

                if (!TourSessions.Any())
                {
                    Message = "Không có phiên tour nào cho tour này.";
                }
                System.Diagnostics.Debug.WriteLine($"BookViewModel initialized with tourId={tourId}, userId={userId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InitializeAsync: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi tải danh sách phiên: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }
        partial void OnTourChanged(Tour value)
        {
            if (value != null)
            {
                InitializeAsync();
            }
        }

        partial void OnSelectedTourSessionChanged(TourSessions value)
        {
            UpdateTotalPrice();
        }

        partial void OnNumberOfTicketsChanged(int value)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            if (SelectedTourSession != null && NumberOfTickets > 0)
            {
                
                TotalPrice = NumberOfTickets * Tour.Price;
                Debug.WriteLine("da chay den dong nay");



            }
            else
            {
                TotalPrice = 0;
            }
        }

        [RelayCommand]
        private async Task BookTickets()
        {
            try
            {
                if (SelectedTourSession == null)
                {
                    Message = "Vui lòng chọn một phiên tour.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                if (NumberOfTickets <= 0)
                {
                    Message = "Vui lòng nhập số vé hợp lệ.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                if (NumberOfTickets > SelectedTourSession.RemainingSeats)
                {
                    Message = $"Chỉ còn {SelectedTourSession.RemainingSeats} ghế trống.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                var booking = new Booking
                {
                    UserId = UserId,
                    TourSessionId = SelectedTourSession.Id,
                    NumberOfPeople = NumberOfTickets,
                    CanReview = 0
                };

                int rowsAffected = await _db.AddBooking(booking);
                if (rowsAffected > 0)
                {
                    SelectedTourSession.RemainingSeats -= NumberOfTickets;
                    await _db.UpdateTourSession(SelectedTourSession);

                    Message = "Mua vé thành công!";
                    await Application.Current.MainPage.DisplayAlert("Success", Message, "OK");
                    await Shell.Current.GoToAsync("///TourDetailPage", true, new Dictionary<string, object>
                    {
                        { "tour", tour }
                    });
                }
                else
                {
                    Message = "Đặt vé thất bại.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in BookTickets: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi đặt vé: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            System.Diagnostics.Debug.WriteLine($"Navigating back to TourDetailPage with tourId={TourId}");
            await Shell.Current.GoToAsync("///TourDetailPage", true, new Dictionary<string, object>
            {
                { "tour", tour }
            });
        }
    }
}