using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CustomerApp;

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

        var supabaseURL = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8000" : "http://localhost:8000";
        var anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyAgCiAgICAicm9sZSI6ICJzZXJ2aWNlX3JvbGUiLAogICAgImlzcyI6ICJzdXBhYmFzZS1kZW1vIiwKICAgICJpYXQiOiAxNjQxNzY5MjAwLAogICAgImV4cCI6IDE3OTk1MzU2MDAKfQ.DaYlNEoUrrEn2Ig7tqibS-PHK5vgusbcbo7X36XVt4Q";
        builder.Services.AddSingleton(new Supabase.Client(supabaseURL, anonKey));
        builder.Services.AddSingleton<ProductService>();
        builder.Services.AddSingleton<PurchaseService>();
        builder.Services.AddSingleton<NavigationService>();
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
        builder.Services.AddSingleton<ProductListPage>();
        builder.Services.AddSingleton<ProductListPageViewModel>();
        builder.Services.AddSingleton<ProductDetailPage>();
        builder.Services.AddSingleton<ProductDetailPageViewModel>();
    }
}