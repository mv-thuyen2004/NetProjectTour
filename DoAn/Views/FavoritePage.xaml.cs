namespace DoAn.Views;
using DoAn.ViewModels;
using DoAn.Services;

public partial class FavoritePage : ContentPage
{
	public FavoritePage()
	{
		InitializeComponent();
        BindingContext = new FavoriteViewModel(new DatabaseServices());
    }
}