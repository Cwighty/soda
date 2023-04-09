using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace CustomerApp.Features.Cart;

[QueryProperty(nameof(IncomingProduct), nameof(IncomingProduct))]
public partial class CartPageViewModel : BaseViewModel
{
    private readonly ICacheService cache;
    private readonly NavigationService navigationService;
    private readonly IMapper mapper;
    private readonly PurchaseService purchaseService;
    private readonly IConfiguration config;
    private readonly UserService userService;
    private Product incomingProduct;
    public Product IncomingProduct
    {
        get => incomingProduct;
        set
        {
            incomingProduct = value;
            if (incomingProduct != null)
            {
                Initialize();
            }
        }
    }

    private ObservableCollection<PurchaseItem> cartItems;
    public ObservableCollection<PurchaseItem> CartItems
    {
        get => cartItems;
        set
        {
            SetProperty(ref cartItems, value);
            var subTotal = 0m;
            foreach (var item in CartItems)
            {
                subTotal += item.CalculatedPrice;
            }
            SubTotal = subTotal;
            Tax = SubTotal * 0.07m;
            Total = SubTotal + Tax;
            OnPropertyChanged(nameof(IsNotEmpty));
        }
    }

    [ObservableProperty]
    private decimal subTotal;
    [ObservableProperty]
    private decimal tax;
    [ObservableProperty]
    private decimal total;

    public bool IsNotEmpty => CartItems?.Count > 0;

    public CartPageViewModel(
        ICacheService cache,
        NavigationService navigationService,
        IMapper mapper,
        PurchaseService purchaseService,
        IConfiguration config,
        UserService userService
        )
    {
        this.cache = cache;
        this.navigationService = navigationService;
        this.mapper = mapper;
        this.purchaseService = purchaseService;
        this.config = config;
        this.userService = userService;
    }

    private void CartItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        var subTotal = 0m;
        foreach (var item in CartItems)
        {
            subTotal += item.CalculatedPrice;
        }
        SubTotal = subTotal;
        Tax = SubTotal * 0.07m;
        Total = SubTotal + Tax;
    }

    public override Task Initialize()
    {
        try
        {
            IsBusy = true;
            var items = cache.Get<ObservableCollection<PurchaseItem>>(nameof(CartItems));
            if (items == null)
            {
                items = new ObservableCollection<PurchaseItem>();
            }
            if (IncomingProduct != null)
            {
                PurchaseItem newPurchase = new();
                mapper.Map(incomingProduct, newPurchase);
                items.Add(newPurchase);
            }
            CartItems = new(items);
            CartItems.CollectionChanged += CartItems_CollectionChanged;
            cache.Add(nameof(CartItems), CartItems);

            IncomingProduct = null;
            return Task.CompletedTask;
        }
        finally
        {
            IsBusy = false;
        }
    }

    public override Task Stop()
    {
        cache.Add(nameof(CartItems), CartItems);
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void ClearCartItem(PurchaseItem item)
    {
        CartItems.Remove(item);
        cache.Add(nameof(CartItems), CartItems);
        OnPropertyChanged(nameof(CartItems));
        OnPropertyChanged(nameof(IsNotEmpty));
    }

    [RelayCommand]
    private async Task ShowPopup()
    {
        if (!userService.IsLoggedIn())
        {
            var options = new string[] { "Sign In", "Create An Account", "Continue As Guest" };
            var action = await Application.Current.MainPage.DisplayActionSheet("", "Cancel", null, options);
            if (action == "Sign In")
            {
                await navigationService.GoTo("///ProfilePage");
                return;
            }
            else if (action == "Create An Account")
            {
                await navigationService.GoTo("///ProfilePage");
                return;
            }
        }
        try
        {
            await purchaseService.CheckoutOnline(CartItems.ToList());
            await navigationService.GoTo(nameof(OrderProcessedPage));
        }
        catch (Exception ex) { }

    }



    [RelayCommand]
    private async Task GoToMenu()
    {
        await navigationService.GoTo($"///MenuPage");
    }
}
