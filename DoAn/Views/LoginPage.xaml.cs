using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            System.Diagnostics.Debug.WriteLine($"BindingContext set: {BindingContext != null}");
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigating to RegisterPage");
            await Shell.Current.GoToAsync("//register");
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Login button clicked. Command: {LoginButton.Command != null}");
        }
    }
}