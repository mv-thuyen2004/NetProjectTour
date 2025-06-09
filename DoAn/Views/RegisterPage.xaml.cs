using DoAn.ViewModels;
namespace DoAn.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            // Logic điều hướng đến trang đăng nhập
            await Shell.Current.GoToAsync("//login");
        }
    }
}