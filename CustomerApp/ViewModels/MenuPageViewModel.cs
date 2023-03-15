using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerApp.Models;
using CustomerApp.Services;

namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
public partial class MenuPageViewModel : BaseViewModel
{
    private readonly ProductService productService;
	public MenuPageViewModel(ProductService productService)
	{
        this.productService = productService;
    }

    [ObservableProperty]
    private List<BaseTypeData> baseTypes;

	[ObservableProperty]
	private List<ProductData> products;

    [ObservableProperty]
    private BaseTypeData selectedBaseType;

    public override async Task Initialize()
    {
        BaseTypes = await productService.GetBaseTypes();
        Products = await productService.GetProducts();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
