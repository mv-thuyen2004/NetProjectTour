
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;
using DoAn.Models;
namespace DoAn.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty] private string searchText;
        [ObservableProperty] private ObservableCollection<Tour> tours;
        [ObservableProperty] private string errorMessage;

        private readonly DatabaseServices _db;

        public HomeViewModel(DatabaseServices db)
        {
            _db = db;
            Tours = new ObservableCollection<Tour>();
            ErrorMessage = string.Empty;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Initializing HomeViewModel");
                await _db.InsertSampleTours();
                await LoadAllToursCommand.ExecuteAsync(null);
                System.Diagnostics.Debug.WriteLine("HomeViewModel initialization completed");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load tours: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Initialize error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task LoadAllTours()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading all tours");
                var allTours = await _db.GetTours();
                Tours.Clear();
                foreach (var tour in allTours)
                {
                    Tours.Add(tour);
                }
                System.Diagnostics.Debug.WriteLine($"Loaded {Tours.Count} tours into ObservableCollection");
                ErrorMessage = Tours.Count == 0 ? "No tours found" : string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load tours: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"LoadAllTours error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task SearchTours(string searchText)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Searching tours with: {searchText}");
                var result = await _db.GetTours(searchText ?? "");
                Tours.Clear();
                foreach (var tour in result)
                {
                    Tours.Add(tour);
                }
                System.Diagnostics.Debug.WriteLine($"Found {Tours.Count} tours for search: {searchText}");
                ErrorMessage = Tours.Count == 0 ? "No tours found for this search" : string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to search tours: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"SearchTours error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task NavigateToTourDetail(Tour tour)
        {
            try
            {
                if (tour == null)
                {
                    System.Diagnostics.Debug.WriteLine("Tour is null");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Navigating to TourDetailPage for tour: {tour.TourName}");
                var navigationParameter = new Dictionary<string, object>
                {
                    { "tour", tour }
                };
                await Shell.Current.GoToAsync("//TourDetailPage", true, navigationParameter);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Navigation error: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"NavigateToTourDetail error: {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }

        [RelayCommand]
        private async Task Tab1(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab1: Staying on HomePage");
        }

        [RelayCommand]
        private async Task Tab2(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab2: Navigating to FavoritesPage");
            await Shell.Current.GoToAsync("//favorite");
        }

        [RelayCommand]
        private async Task Tab3(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab3: Navigating to BookingPage");
            await Shell.Current.GoToAsync("//booking", true, new Dictionary<string, object>
            {
                { "db", new DatabaseServices() }
            });
        }

        [RelayCommand]
        private async Task Tab4(ContentPage page)
        {
            System.Diagnostics.Debug.WriteLine("Tab4: Navigating to ProfilePage");
            await Shell.Current.GoToAsync("//profile");
        }
    }
}