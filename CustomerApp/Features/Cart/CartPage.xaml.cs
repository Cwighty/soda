namespace CustomerApp.Features.Cart;

public partial class CartPage : BasePage
{
	public CartPage(CartPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}