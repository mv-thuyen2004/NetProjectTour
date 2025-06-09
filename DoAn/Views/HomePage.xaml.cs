using DoAn.ViewModels;
using System.Diagnostics;

namespace DoAn.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomeViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel;
                System.Diagnostics.Debug.WriteLine("HomePage initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in HomePage initialization: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                Application.Current.MainPage.DisplayAlert("Error", $"Initialization failed: {ex.Message}", "OK");
            }
        }
    }
}