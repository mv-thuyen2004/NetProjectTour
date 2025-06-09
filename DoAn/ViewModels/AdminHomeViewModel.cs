using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace DoAn.ViewModels
{
    public partial class AdminHomeViewModel : ObservableObject
    {
        

        [RelayCommand]
        private async Task ManageAccounts()
        {
            await Shell.Current.GoToAsync("///manageaccounts");
        }

        [RelayCommand]
        private async Task ManageTours()
        {
            await Shell.Current.GoToAsync("///managetours");
        }

        [RelayCommand]
        private async Task ManageTourStatus()
        {
            await Shell.Current.GoToAsync("///manageSessions");
        }
        [RelayCommand]
        private async Task Tab1()
        {
            System.Diagnostics.Debug.WriteLine("Tab1: Staying on HomePage");
        }

        [RelayCommand]
        private async Task Tab2()
        {
            System.Diagnostics.Debug.WriteLine("Tab2: Navigating to FavoritesPage");
            await Shell.Current.GoToAsync("//report");
        }

        

        [RelayCommand]
        private async Task Tab3()
        {
            System.Diagnostics.Debug.WriteLine("Tab3: Navigating to ProfilePage");
            await Shell.Current.GoToAsync("//profileAdmin");
        }
    }
}