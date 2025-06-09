using Microsoft.Maui.Controls;
using DoAn.ViewModels;

namespace DoAn.Views
{
    public partial class ReviewPopupPage : ContentPage
    {
        private readonly BookingViewModel _viewModel;

        public ReviewPopupPage(BookingViewModel viewModel)
        {
            System.Diagnostics.Debug.WriteLine("ReviewPopupPage: Constructor called.");
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel; // Đảm bảo BindingContext được đặt đúng
            System.Diagnostics.Debug.WriteLine("ReviewPopupPage: BindingContext set to BookingViewModel.");
        }

        // Phương thức xử lý sự kiện khi thay đổi lựa chọn trong Picker
        private void OnRatingSelected(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ReviewPopupPage: OnRatingSelected triggered.");
            if (sender is Picker picker)
            {
                // Lấy giá trị đã chọn từ Picker (index hoặc giá trị thực)
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex >= 0 && selectedIndex < _viewModel.RatingOptions.Count)
                {
                    _viewModel.SelectedRating = _viewModel.RatingOptions[selectedIndex];
                    System.Diagnostics.Debug.WriteLine($"Selected Rating: {_viewModel.SelectedRating}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ReviewPopupPage: Invalid selected index in Picker.");
                }
            }
        }
    }
}