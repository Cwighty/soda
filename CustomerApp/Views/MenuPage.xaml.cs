using CustomerApp.ViewModels;

namespace CustomerApp.Views;

public partial class MenuPage : BasePage
{
	public MenuPage(MenuPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}