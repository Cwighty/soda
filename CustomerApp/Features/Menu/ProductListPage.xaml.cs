namespace CustomerApp.Features.Menu.Controls;

public partial class ProductListPage : BasePage
{
	public ProductListPage(ProductListPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}