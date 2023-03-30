

namespace CustomerApp.Features.Menu;

public partial class MenuPageViewModel : BaseViewModel
{
    private readonly IProductService productService;
    private readonly NavigationService navigationService;

    public MenuPageViewModel(IProductService productService, NavigationService navigationService)
    {
        this.productService = productService;
        this.navigationService = navigationService;
    }

    [ObservableProperty]
    private List<BaseType> baseTypes;

    [ObservableProperty]
    private List<BaseProduct> productsByBase;

    [ObservableProperty]
    private List<BaseProduct> filteredProducts;

    [ObservableProperty]
    private List<Product> products;

    private BaseType selectedBaseType;
    public BaseType SelectedBaseType
    {
        get
        {
            return selectedBaseType;
        }

        set
        {
            SetProperty(ref selectedBaseType, value);
            FilteredProducts = ProductsByBase?.Where(
                pb => pb.Products.Any(p => p.Base.TypeId == value.Id)
                ).ToList();
        }
    }

    public override async Task Initialize()
    {
        BaseTypes = await productService.GetBaseTypes();
        ProductsByBase = await productService.GetBaseProducts();
        SelectedBaseType = BaseTypes.First();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Navigate(List<Product> products)
    {
        await navigationService
            .GoTo(
                nameof(ProductListPage),
                new Dictionary<string, object>()
                {
                    ["Products"] = products
                });
    }

    [RelayCommand]
    private async Task NavigateToDetail(Product product)
    {
        await navigationService
            .GoTo(
                nameof(ProductDetailPage),
                new Dictionary<string, object>()
                {
                    ["Product"] = product
                });
    }
}
