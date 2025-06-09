using Microsoft.Extensions.Logging;

//namespace DoAn
//{
//    public static class MauiProgram
//    {
//        public static MauiApp CreateMauiApp()
//        {
//            var builder = MauiApp.CreateBuilder();
//            builder
//                .UseMauiApp<App>()
//                .ConfigureFonts(fonts =>
//                {
//                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//                });

//#if DEBUG
//    		builder.Logging.AddDebug();
//#endif

//            return builder.Build();
//        }
//    }
//}


using DoAn.Services;
using DoAn.ViewModels;
using DoAn.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
namespace DoAn
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DatabaseServices>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<TourDetailViewModel>();
            builder.Services.AddTransient<TourDetailPage>();
            builder.Services.AddTransient<FavoriteViewModel>();
            builder.Services.AddTransient<FavoritePage>();
            builder.Services.AddTransient<BookingViewModel>();
            builder.Services.AddTransient<BookingPage>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<ProfilePage>();

            return builder.Build();
        }
    }
}