using CommunityToolkit.Mvvm.Input;
using Size = CustomerApp.Models.Size;

namespace CustomerApp.ViewModels;

[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailPageViewModel : BaseViewModel
{
    private readonly IProductService productService;
    private readonly NavigationService navigationService;
    [ObservableProperty]
    private Product product;
    [ObservableProperty]
    private Product customizedProduct;

    [ObservableProperty]
    private List<Size> productSizes;
    private Size selectedProductSize;
    public Size SelectedProductSize
    {
        get { return selectedProductSize; }
        set
        {
            SetProperty(ref selectedProductSize, value);
            foreach (var size in ProductSizes)
            {
                size.IsSelected = size == value;
            }
            CustomizedProduct.Size = value;
        }
    }

    [ObservableProperty]
    private List<AddOn> addOns;
    [ObservableProperty]
    private AddOn selectedAddOn;

    public ProductDetailPageViewModel(IProductService productService, NavigationService navigationService)
    {
        
        this.productService = productService;
        this.navigationService = navigationService;
    }

    public override async Task Initialize()
    {
        AddOns = await productService.GetAddOns();
        ProductSizes = Product.Base.BaseType.Sizes.ToList();
        CustomizedProduct = Product;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task ClearAddOn(AddOn addOn)
    {
        CustomizedProduct.AddOns.Remove(addOn);
        OnPropertyChanged(nameof(CustomizedProduct));
    }

    [RelayCommand]
    private async Task AddAddOn()
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
}
