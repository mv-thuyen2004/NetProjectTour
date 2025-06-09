using DoAn.Services;
using DoAn.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DoAn.Views
{
    [QueryProperty(nameof(TourId), "tourId")]
    public partial class EditTourPage : ContentPage
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

        public EditTourPage(DatabaseServices db)
        {
            InitializeComponent();
            BindingContext = new EditTourViewModel(db);
        }

        private void OnTourIdReceived(string tourId)
        {
            if (BindingContext is EditTourViewModel viewModel)
            {
                viewModel.LoadTourData(tourId);
            }
        }
    }
}