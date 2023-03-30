namespace CustomerApp.Features.Menu;

public partial class MenuPage : BasePage
{
	public MenuPage(MenuPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}