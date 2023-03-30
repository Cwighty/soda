namespace CustomerApp.Features.Cart;

public partial class PaymentPage : BasePage
{
	public PaymentPage(PaymentPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}