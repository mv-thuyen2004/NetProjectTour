using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class ManageAccountsPage : ContentPage
    {
        public ManageAccountsPage(DatabaseServices db)
        {
            InitializeComponent();
            BindingContext = new ManageAccountsViewModel(db);
        }
    }
}