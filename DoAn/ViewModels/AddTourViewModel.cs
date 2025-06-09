using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;
using System.Diagnostics;
using System.IO;
using Microsoft.Maui.Controls;

namespace DoAn.ViewModels
{
    public partial class AddTourViewModel : ObservableObject
    {
        [ObservableProperty] string tourName;
        [ObservableProperty] string location;
        [ObservableProperty] string description;
        [ObservableProperty] string imageUrl;
        [ObservableProperty] int price;
        [ObservableProperty] float avgRate;
        [ObservableProperty] int totalBooked;
        [ObservableProperty] string message;

        private readonly DatabaseServices _db;

        public AddTourViewModel(DatabaseServices db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            Message = "Vui lòng nhập thông tin tour mới.";
        }

        [RelayCommand]
        private async Task PickImage()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Chọn ảnh cho tour",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    string fileName = $"tour_{DateTime.Now.Ticks}.jpg";
                    string targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

                    using (var sourceStream = await result.OpenReadAsync())
                    using (var targetStream = File.Create(targetPath))
                    {
                        await sourceStream.CopyToAsync(targetStream);
                    }

                    ImageUrl = fileName;
                    Message = $"Đã chọn ảnh: {fileName}";
                    await Application.Current.MainPage.DisplayAlert("Success", Message, "OK");
                }
                else
                {
                    Message = "Không có ảnh nào được chọn.";
                    await Application.Current.MainPage.DisplayAlert("Info", Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in PickImage: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi chọn ảnh: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }

        [RelayCommand]
        private async Task AddTour()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TourName) || string.IsNullOrWhiteSpace(Location) ||
                    string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(ImageUrl))
                {
                    Message = "Vui lòng nhập đầy đủ thông tin và chọn ảnh.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                bool success = await _db.AddTour(TourName, Location, Description, ImageUrl, Price, AvgRate, TotalBooked);
                Message = success ? "Thêm tour thành công!" : "Thêm tour thất bại!";

                await Application.Current.MainPage.DisplayAlert(Message.Contains("thành công") ? "Success" : "Error", Message, "OK");

                if (Message.Contains("thành công"))
                {
                    Debug.WriteLine($"Tour added successfully: {TourName}");
                    // Gửi thông báo để làm mới danh sách
                    MessagingCenter.Send(this, "TourAdded");
                    await Shell.Current.GoToAsync("//managetours");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddTour: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Thêm tour thất bại do lỗi: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
        }
        [RelayCommand]
        private async Task Back()
        {
            System.Diagnostics.Debug.WriteLine("BackCommand executed");
            await Shell.Current.GoToAsync("///adminhome");
        }
    }
}