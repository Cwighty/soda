namespace CustomerApp.Views;

public partial class PaymentPage : BasePage
{
	public PaymentPage(PaymentPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}