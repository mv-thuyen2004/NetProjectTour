using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class TourDetailPage : ContentPage
    {
        public TourDetailPage()
        {
            InitializeComponent();
            BindingContext = new TourDetailViewModel(new DatabaseServices());
        }
    }
}