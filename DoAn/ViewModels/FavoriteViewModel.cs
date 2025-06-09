using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Models;
using DoAn.Services;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DoAn.ViewModels
{
    public partial class FavoriteViewModel : ObservableObject
    {

        private readonly DatabaseServices _db;
        private readonly int _userId;

        [ObservableProperty]
        private ObservableCollection<Tour> favoriteTours = new();

        public FavoriteViewModel(DatabaseServices db)
        {
            _db = db;
            var userIdString = Preferences.Get("IdNguoiDung", "0");
            if (!int.TryParse(userIdString, out _userId) || _userId == 0)
            {
                Debug.WriteLine("Error: Invalid or missing UserId in Preferences");
                _userId = 1; // Fallback cho test
            }
            Debug.WriteLine($"FavoritesViewModel initialized, UserId: {_userId}");
            LoadFavoriteTours();
        }

        private async void LoadFavoriteTours()
        {
            try
            {
                var tours = await _db.GetFavoriteTours(_userId);
                FavoriteTours.Clear();
                foreach (var tour in tours)
                {
                    FavoriteTours.Add(tour);
                }
                Debug.WriteLine($"Loaded {FavoriteTours.Count} favorite tours for UserId: {_userId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadFavoriteTours error: {ex.Message}, StackTrace: {ex.StackTrace}");
                await Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể tải danh sách tour yêu thích: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToTourDetail(Tour tour)
        {
            if (tour == null)
            {
                Debug.WriteLine("NavigateToTourDetail: Tour is null");
                return;
            }

            Debug.WriteLine($"Navigating to TourDetailPage for Tour: {tour.TourName}, TourId: {tour.TourId}");
            await Shell.Current.GoToAsync("//TourDetailPage", new Dictionary<string, object>
            {
                { "tour", tour }
            });
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
            
        }

        [RelayCommand]
        private async Task Tab3(ContentPage page)
        {
            // Điều hướng đến trang Giỏ hàng (giả sử route là "CartPage")
            await Shell.Current.GoToAsync("///booking");
        }

        [RelayCommand]
        private async Task Tab4(ContentPage page)
        {
            // Điều hướng đến trang Hồ sơ (giả sử route là "ProfilePage")
            await Shell.Current.GoToAsync("///profile");
        }
    }
}
