using DoAn.Services;
using DoAn.ViewModels;
using Microsoft.Maui.Controls;

namespace DoAn.Views
{
    public partial class ManageSessionsPage : ContentPage
    {
        public ManageSessionsPage(DatabaseServices db)
        {
            InitializeComponent();
            BindingContext = new ManageSessionsViewModel(db);
        }
    }
}