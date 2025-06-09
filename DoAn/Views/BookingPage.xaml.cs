namespace DoAn.Views;
using DoAn.ViewModels;
using DoAn.Services;
public partial class BookingPage : ContentPage
{
    public BookingPage(DatabaseServices db)
    {
        System.Diagnostics.Debug.WriteLine("BookingPage: Constructor called.");
        InitializeComponent();
        BindingContext = new BookingViewModel(db);
        System.Diagnostics.Debug.WriteLine("BookingPage: BindingContext set to BookingViewModel.");
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("BookingPage: CollectionView_SelectionChanged triggered.");
        if (BindingContext is BookingViewModel viewModel && e.CurrentSelection.FirstOrDefault() is BookingDetail selectedItem)
        {
            viewModel.SelectedBooking = selectedItem;
            System.Diagnostics.Debug.WriteLine($"Selected Booking: {selectedItem.BookingId}, CanReview: {selectedItem.CanReview}");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("BookingPage: No valid selection in CollectionView_SelectionChanged.");
        }
    }

}