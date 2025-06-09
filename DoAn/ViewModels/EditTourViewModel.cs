using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoAn.Services;
using System.Diagnostics;
using System.IO;
using DoAn.Models;
using DoAn.Views;

namespace DoAn.ViewModels
{
    
    public partial class EditTourViewModel : ObservableObject
    {
        [ObservableProperty] string tourName;
        [ObservableProperty] string location;
        [ObservableProperty] string description;
        [ObservableProperty] string imageUrl;
        [ObservableProperty] decimal price;
        [ObservableProperty] float avgRate;
        [ObservableProperty] int totalBooked;
        [ObservableProperty] string message;

        

        private Tour _originalTour;
        private readonly DatabaseServices _db;

        public EditTourViewModel(DatabaseServices db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _originalTour = null;
            
        }

        public async void LoadTourData(string tourId)
        {
            try
            {
                if (int.TryParse(tourId, out int id))
                {
                    var tour = await _db.GetTourById(id);
                    if (tour != null)
                    {
                        _originalTour = tour;
                        TourName = tour.TourName;
                        Location = tour.Location;
                        Description = tour.Description;
                        ImageUrl = tour.ImageUrl;
                        Price = tour.Price;
                        AvgRate = tour.AvgRate;
                        TotalBooked = tour.TotalBooked;
                        Message = "Đã tải thông tin tour để chỉnh sửa.";
                        await Application.Current.MainPage.DisplayAlert("Info", Message, "OK");
                    }
                    else
                    {
                        Message = "Không tìm thấy tour để chỉnh sửa.";
                        await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    }
                }
                else
                {
                    Message = "ID tour không hợp lệ.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadTourData: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Lỗi khi tải dữ liệu: {ex.Message}";
                await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
            }
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
        private async Task UpdateTour()
        {
            try
            {
                if (_originalTour == null)
                {
                    Message = "Không tìm thấy thông tin tour để cập nhật.";
                    await Application.Current.MainPage.DisplayAlert("Error", Message, "OK");
                    return;
                }

                var tour = new Tour
                {
                    TourId = _originalTour.TourId,
                    TourName = !string.IsNullOrWhiteSpace(TourName) && TourName != _originalTour.TourName ? TourName : _originalTour.TourName,
                    Location = !string.IsNullOrWhiteSpace(Location) && Location != _originalTour.Location ? Location : _originalTour.Location,
                    Description = !string.IsNullOrWhiteSpace(Description) && Description != _originalTour.Description ? Description : _originalTour.Description,
                    ImageUrl = !string.IsNullOrWhiteSpace(ImageUrl) && ImageUrl != _originalTour.ImageUrl ? ImageUrl : _originalTour.ImageUrl,
                    Price = Price != 0 && Price != _originalTour.Price ? Price : _originalTour.Price,
                    AvgRate = _originalTour.AvgRate,
                    TotalBooked = _originalTour.TotalBooked
                };

                bool success = await _db.UpdateTour(tour);
                Message = success ? "Cập nhật tour thành công!" : "Cập nhật tour thất bại!";

                await Application.Current.MainPage.DisplayAlert(Message.Contains("thành công") ? "Success" : "Error", Message, "OK");

                if (Message.Contains("thành công"))
                {
                    Debug.WriteLine($"Tour updated successfully: {TourName}");
                    await Shell.Current.GoToAsync("//managetours");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateTour: {ex.Message}, StackTrace: {ex.StackTrace}");
                Message = $"Cập nhật tour thất bại do lỗi: {ex.Message}";
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