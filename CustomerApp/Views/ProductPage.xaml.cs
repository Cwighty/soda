using CustomerApp.ViewModels;

namespace CustomerApp.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage(ProductPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}