
using DoAn.ViewModels;
namespace DoAn.Views
{
    public partial class AdminHomePage : ContentPage
    {
        public AdminHomePage()
        {
            InitializeComponent();
            BindingContext = new AdminHomeViewModel();
        }
    }
}