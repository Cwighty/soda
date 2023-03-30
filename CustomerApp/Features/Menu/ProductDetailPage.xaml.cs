namespace CustomerApp.Features.Menu;

public partial class ProductDetailPage : BasePage
{
	public ProductDetailPage(ProductDetailPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}