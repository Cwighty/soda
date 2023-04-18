using CustomerApp.Features.Favorites;

namespace CustomerApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ProductListPage), typeof(ProductListPage));
        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
        Routing.RegisterRoute(nameof(OrderConfirmationPage), typeof(OrderConfirmationPage));
        Routing.RegisterRoute(nameof(OrderProcessedPage), typeof(OrderProcessedPage));
        Routing.RegisterRoute(nameof(ProfileDetailsPage), typeof(ProfileDetailsPage));
        Routing.RegisterRoute(nameof(OrderHistoryPage), typeof(OrderHistoryPage));
        Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));
    }
}