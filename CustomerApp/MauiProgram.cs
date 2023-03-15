using AppActions.Icons.Maui;
using CommunityToolkit.Maui;
using CustomerApp.Services;
using CustomerApp.ViewModels;
using CustomerApp.Views;
using Microsoft.Extensions.Logging;

namespace CustomerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new Supabase.Client("http://10.0.2.2:8000", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyAgCiAgICAicm9sZSI6ICJzZXJ2aWNlX3JvbGUiLAogICAgImlzcyI6ICJzdXBhYmFzZS1kZW1vIiwKICAgICJpYXQiOiAxNjQxNzY5MjAwLAogICAgImV4cCI6IDE3OTk1MzU2MDAKfQ.DaYlNEoUrrEn2Ig7tqibS-PHK5vgusbcbo7X36XVt4Q"));
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<PurchaseService>();
            IntializePages(builder);


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void IntializePages(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MenuPage>();
            builder.Services.AddSingleton<MenuPageViewModel>();
            builder.Services.AddSingleton<FeaturePage>();
            builder.Services.AddSingleton<FeaturePageViewModel>();
            builder.Services.AddSingleton<CartPage>();
            builder.Services.AddSingleton<CartPageViewModel>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<ProfilePageViewModel>();
        }
    }
}