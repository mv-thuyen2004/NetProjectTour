using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class AddTourPage : ContentPage
    {
        public AddTourPage(DatabaseServices db)
        {
            InitializeComponent();
            BindingContext = new AddTourViewModel(db);
        }
    }
}