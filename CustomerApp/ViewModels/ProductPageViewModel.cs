using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerApp.Models;
using CustomerApp.Services;

namespace CustomerApp.ViewModels;

public partial class ProductPageViewModel : ObservableObject
{
	public ProductPageViewModel(ProductService productService)
	{
        this.productService = productService;
    }

	[ObservableProperty]
	private List<ProductData> products;
    private readonly ProductService productService;

    [RelayCommand]
	private async Task Loaded()
	{
		Products = await productService.GetProducts();
	}
}
