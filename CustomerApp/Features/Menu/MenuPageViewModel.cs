namespace CustomerApp.Features.Menu;

public partial class MenuPageViewModel : BaseViewModel
{
    private readonly IProductService productService;
    private readonly INavigationService navigationService;

    public MenuPageViewModel(IProductService productService, INavigationService navigationService)
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
                pb => pb.Products.Any(p => p.Base.BaseTypeId == value.Id)
                ).ToList();
        }
    }

    public override async Task Initialize()
    {
        try
        {
            IsBusy = true;
            BaseTypes = await productService.GetBaseTypes();
            ProductsByBase = await productService.GetBaseProducts();
            SelectedBaseType = BaseTypes?.First();
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
    public async Task Navigate(List<Product> products)
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
    public async Task NavigateToDetail(Product product)
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
