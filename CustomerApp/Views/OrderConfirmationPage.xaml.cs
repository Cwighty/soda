namespace CustomerApp.Views;

public partial class OrderConfirmationPage : BasePage
{
	public OrderConfirmationPage(OrderConfirmationPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}