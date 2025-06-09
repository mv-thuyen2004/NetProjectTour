namespace DoAn.Views;
using DoAn.ViewModels;
public partial class ProfileAdminpage : ContentPage
{
	public ProfileAdminpage()
	{
		InitializeComponent();
        BindingContext = new ProfileAdminViewModel();
    }
}