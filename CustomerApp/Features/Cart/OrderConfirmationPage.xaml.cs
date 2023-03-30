namespace CustomerApp.Features.Cart;

public partial class OrderConfirmationPage : BasePage
{
	public OrderConfirmationPage(OrderConfirmationPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}