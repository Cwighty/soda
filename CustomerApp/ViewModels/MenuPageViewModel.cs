using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
public partial class MenuPageViewModel : BaseViewModel
{
    private readonly ProductService productService;
    private readonly NavigationService navigationService;

    public MenuPageViewModel(ProductService productService, NavigationService navigationService)
	{
        this.productService = productService;
        this.navigationService = navigationService;
    }

    [ObservableProperty]
    private List<BaseTypeData> baseTypes;

    [ObservableProperty]
    private List<BaseProduct> productsByBase;

    [ObservableProperty]
    private List<BaseProduct> filteredProducts;

    [ObservableProperty]
	private List<ProductData> products;

    private BaseTypeData selectedBaseType;
    public BaseTypeData SelectedBaseType
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
        SelectedBaseType = BaseTypes.First();
        ProductsByBase = await productService.GetBaseProducts();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Navigate(List<ProductData> products)
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
    private async Task NavigateToDetail(ProductData product)
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
