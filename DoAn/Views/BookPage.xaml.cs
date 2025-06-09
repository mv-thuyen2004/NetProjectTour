using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class BookPage : ContentPage
    {
        public BookPage()
        {
            InitializeComponent();
            BindingContext = new BookViewModel(new DatabaseServices());
        }
    }
}