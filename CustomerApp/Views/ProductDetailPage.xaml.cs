namespace CustomerApp.Views;

public partial class ProductDetailPage : BasePage
{
	public ProductDetailPage(ProductDetailPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}