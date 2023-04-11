using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using MonkeyCache.FileStore;
using SodaShared.Mappers;
using Supabase;
using System;
using System.Reflection;

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
                fonts.AddFont("Montserrat-Thin.ttf", "MontserratThin");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
            });

        LoadAppsettingsIntoConfig(builder);

        Barrel.ApplicationId = "MonkeyCash1";
        var supabaseURL = builder.Configuration["SupabaseURL"];
        var anonKey = builder.Configuration["SupabaseAnonKey"];

        builder.Services.AddAutoMapper(typeof(MapperProfile));

        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true,
        };
        builder.Services.AddSingleton(new Supabase.Client(supabaseURL, anonKey, options));
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<PurchaseService>();

        var storeApiUrl = builder.Configuration["StoreAPI"];
        if (string.IsNullOrEmpty(storeApiUrl))
        {
            storeApiUrl = Environment.GetEnvironmentVariable("VS_TUNNEL_URL");
        }
        builder.Services.AddHttpClient(
            "StoreAPI", client => client.BaseAddress = new Uri(storeApiUrl)
        );

        IntializePages(builder);


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    
    private static void LoadAppsettingsIntoConfig(MauiAppBuilder builder)
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("CustomerApp.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);
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
        builder.Services.AddSingleton<OrderProcessedPage>();
        builder.Services.AddSingleton<OrderProcessedPageViewModel>();
    }
}