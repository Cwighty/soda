namespace CustomerApp.Views.Controls;

public partial class ProductListPage : BasePage
{
	public ProductListPage(ProductListPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}