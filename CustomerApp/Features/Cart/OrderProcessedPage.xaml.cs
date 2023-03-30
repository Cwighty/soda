namespace CustomerApp.Features.Cart;

public partial class OrderProcessedPage : BasePage
{
	public OrderProcessedPage(OrderProcessedPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}