namespace CustomerApp.Views;

public partial class CartPage : BasePage
{
	public CartPage(CartPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}