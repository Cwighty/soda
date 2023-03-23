using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CustomerApp.ViewModels;

[QueryProperty(nameof(IncomingProduct), nameof(IncomingProduct))]
public partial class CartPageViewModel : BaseViewModel
{
    private readonly CacheService cache;

    public Product IncomingProduct { get; set; }

    [ObservableProperty]
    private ObservableCollection<Product> cartItems;
    public CartPageViewModel(CacheService cache)
    {
        this.cache = cache;
    }
    public override Task Initialize()
    {
        var items = cache.Get<ObservableCollection<Product>>(nameof(cartItems));
        if (items == null)
        {
            CartItems = new ObservableCollection<Product>();
        }
        else
        {
            CartItems = items;
        }
        if (IncomingProduct != null)
            CartItems.Add(IncomingProduct);
        IncomingProduct = null;
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        cache.Add(nameof(cartItems), cartItems);
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void ClearCartItem(Product product)
    {
        CartItems.Remove(product);
        OnPropertyChanged(nameof(CartItems));
    }
}
