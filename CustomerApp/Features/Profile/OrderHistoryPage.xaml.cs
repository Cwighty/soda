namespace CustomerApp.Features.Profile;

public partial class OrderHistoryPage : BasePage
{
	public OrderHistoryPage(OrderHistoryPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}