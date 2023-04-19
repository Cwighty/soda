using CustomerApp.Features.Favorites;
using Size = SodaShared.Models.Size;

namespace CustomerApp.Features.Menu;

[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailPageViewModel : BaseViewModel
{
    private readonly IProductService productService;
    private readonly NavigationService navigationService;
    private readonly FavoritesService favoritesService;
    [ObservableProperty]
    private Product product;
    [ObservableProperty]
    private Product customizedProduct;
    [ObservableProperty]
    private List<Size> productSizes;
    [ObservableProperty]
    private string isFavoriteImg;
    
    private Size selectedProductSize;
    public Size SelectedProductSize
    {
        get { return selectedProductSize; }
        set
        {
            SetProperty(ref selectedProductSize, value);
            CustomizedProduct.Size = value;
        }
    }

    [ObservableProperty]
    private List<AddOn> addOns;
    [ObservableProperty]
    private AddOn selectedAddOn;

    public ProductDetailPageViewModel(IProductService productService,
                                      NavigationService navigationService,
                                      FavoritesService favoritesService)
    {
        this.productService = productService;
        this.navigationService = navigationService;
        this.favoritesService = favoritesService;
    }

    public override async Task Initialize()
    {
        try
        {
            IsBusy = true;
            AddOns = await productService.GetAddOns();
            ProductSizes = Product.Base.BaseType.Sizes.ToList();
            if (Product != null)
                CustomizedProduct = Product;
            await UpdateFavoriteImage();
        }
        finally
        {
            IsBusy = false;
        }
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void ClearAddOn(AddOn addOn)
    {
        CustomizedProduct.AddOns.Remove(addOn);
        OnPropertyChanged(nameof(CustomizedProduct));
    }

    [RelayCommand]
    private void AddAddOn()
    {
        if (SelectedAddOn != null)
        {
            CustomizedProduct.AddOns.Add(SelectedAddOn);
            OnPropertyChanged(nameof(CustomizedProduct));
            SelectedAddOn = null;
        }
    }

    [RelayCommand]
    private async Task AddToCart()
    {
        await navigationService.ClearStack();
        await navigationService.GoTo("///CartPage", 
            new Dictionary<string, object>()
            {
                ["IncomingProduct"] = CustomizedProduct
            });
    }

    [RelayCommand]
    private async Task AddToFavorites()
    {
        await favoritesService.ToggleFavorite(Product.Id);
        await UpdateFavoriteImage();
    }

    private async Task UpdateFavoriteImage()
    {
        var isFav = await favoritesService.IsFavorite(Product.Id);
        if (isFav)
        {
            IsFavoriteImg = "heart_filled.png";
        }
        else
        {
            IsFavoriteImg = "heart.png";
        }
    }
}
