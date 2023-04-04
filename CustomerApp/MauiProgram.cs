using CommunityToolkit.Maui;
using CustomerApp.Mappers;
using Microsoft.Extensions.Logging;
using MonkeyCache.FileStore;
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

        Barrel.ApplicationId = "MonkeyCash4";

        builder.Services.AddAutoMapper(typeof(MapperProfile));

        var supabaseURL = "https://dyafwhkcifxogstvfsuc.supabase.co";
        var anonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImR5YWZ3aGtjaWZ4b2dzdHZmc3VjIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODA1Nzk2MjIsImV4cCI6MTk5NjE1NTYyMn0.IjDLttzrFOYyYRGqFodGHjtu6NbjpH7idZRLglovEzE";
        builder.Services.AddSingleton(new Supabase.Client(supabaseURL, anonKey));
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<PurchaseService>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<OrderService>();

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
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<RegisterPageViewModel>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<PaymentPage>();
        builder.Services.AddSingleton<PaymentPageViewModel>();
        builder.Services.AddSingleton<ProfileDetailsPage>();
        builder.Services.AddSingleton<ProfileDetailsPageViewModel>();
        builder.Services.AddSingleton<OrderHistoryPage>();
        builder.Services.AddSingleton<OrderHistoryPageViewModel>();
    }
}