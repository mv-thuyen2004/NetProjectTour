using DoAn.Services;
using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class ManageToursPage : ContentPage
    {
        private readonly ManageToursViewModel _viewModel;

        public ManageToursPage(DatabaseServices db)
        {
            InitializeComponent();
            _viewModel = new ManageToursViewModel(db);
            BindingContext = _viewModel;

            // L?ng nghe th�ng b�o khi tour ???c th�m
            MessagingCenter.Subscribe<AddTourViewModel>(this, "TourAdded", async (sender) =>
            {
                await _viewModel.RefreshToursAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}