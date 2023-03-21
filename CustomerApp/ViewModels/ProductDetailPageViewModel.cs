using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.ViewModels;

[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailPageViewModel : BaseViewModel
{
    private readonly ProductService productService;

    [ObservableProperty]
    private ProductData product;
    [ObservableProperty]
    private ProductData customizedProduct;

    [ObservableProperty]
    private List<ProductSize> productSizes;
    private ProductSize selectedProductSize;
    public ProductSize SelectedProductSize
    {
        get { return selectedProductSize; }
        set
        {
            SetProperty(ref selectedProductSize, value);
            foreach (var product in ProductSizes)
            {
                product.IsSelected = product == value;
            }
        }
    }

    [ObservableProperty]
    private List<AddOnData> addOns;
    [ObservableProperty]
    private AddOnData selectedAddOn;

    public ProductDetailPageViewModel(ProductService productService)
    {
        ProductSizes = new() {
            new ProductSize("Small", "drink_small.png"),
            new ProductSize("Medium", "drink_medium.png"),
            new ProductSize("Large", "drink_large.png")
        };
        this.productService = productService;
    }

    public override async Task Initialize()
    {
        AddOns = await productService.GetAddOns();
        CustomizedProduct = Product;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task ClearAddOn(AddOnData addOn)
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
}
