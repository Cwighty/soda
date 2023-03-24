using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CustomerApp.ViewModels;

[QueryProperty(nameof(IncomingProduct), nameof(IncomingProduct))]
public partial class CartPageViewModel : BaseViewModel
{
    private readonly CacheService cache;

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

    public CartPageViewModel(CacheService cache)
    {
        this.cache = cache;
    }
    public override Task Initialize()
    {
        var items = cache.Get<ObservableCollection<Product>>(nameof(CartItems));
        if (items == null)
        {
            items = new ObservableCollection<Product>();
            if (IncomingProduct != null)
                items.Add(IncomingProduct);
            CartItems = new(items);
        }
        else
        {
            if (IncomingProduct != null)
                items.Add(IncomingProduct);
            CartItems = new(items);
        }

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
