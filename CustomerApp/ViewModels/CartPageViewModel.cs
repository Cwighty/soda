using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace CustomerApp.ViewModels;

[QueryProperty(nameof(IncomingProduct), nameof(IncomingProduct))]
public partial class CartPageViewModel : BaseViewModel
{
    private readonly ICacheService cache;
    private readonly NavigationService navigationService;

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

    [ObservableProperty]
    private ObservableCollection<Product> cartItems;
    private Product incomingProduct;

    public CartPageViewModel(ICacheService cache, NavigationService navigationService)
    {
        this.cache = cache;
        this.navigationService = navigationService;
    }
    public override Task Initialize()
    {
        var items = cache.Get<ObservableCollection<Product>>(nameof(CartItems));
        if (items == null)
        {
            items = new ObservableCollection<Product>();
        }
        if (IncomingProduct != null)
        {
            items.Add(IncomingProduct);
            CartItems = new(items);
        }
        
        IncomingProduct = null;
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        if (CartItems != null)
        {
            cache.Add(nameof(CartItems), CartItems);
        }
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private void ClearCartItem(Product product)
    {
        CartItems.Remove(product);
        OnPropertyChanged(nameof(CartItems));
    }

    [RelayCommand]
    private async Task ShowPopup() {



        var options = new string[] { "Sign In", "Create An Account", "Continue As Guest" };
        var action = await Application.Current.MainPage.DisplayActionSheet("", "Cancel", null, options);
        if (action == "Sign In")
        {
            // Do something
        }
        else if (action == "Create An Account")
        {
            // Do something else
        }
        else if (action == "Continue As Guest")
        {
           await navigationService.GoTo(nameof(PaymentPage));
        }

    }    
    
}
