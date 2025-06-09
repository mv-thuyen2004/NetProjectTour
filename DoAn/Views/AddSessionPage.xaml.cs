using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views
{
    [QueryProperty(nameof(TourId), "tourId")]
    public partial class AddSessionPage : ContentPage
    {
        private string _tourId;
        public string TourId
        {
            get => _tourId;
            set
            {
                _tourId = value;
                OnTourIdReceived(_tourId);
            }
        }

        public AddSessionPage(DatabaseServices db)
        {
            InitializeComponent();
            BindingContext = new AddSessionViewModel(db);
        }

        private void OnTourIdReceived(string tourId)
        {
            if (BindingContext is AddSessionViewModel viewModel)
            {
                viewModel.SetTourId(tourId);
            }
        }
    }
}