using DoAn.ViewModels;
using DoAn.Services;

namespace DoAn.Views
{
    public partial class AdminReportPage : ContentPage
    {
        public AdminReportPage()
        {
            InitializeComponent();
            var db = new DatabaseServices();
            BindingContext = new AdminReportViewModel(db);
        }
    }
}