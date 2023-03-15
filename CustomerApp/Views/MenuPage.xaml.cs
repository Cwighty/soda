using CustomerApp.ViewModels;

namespace CustomerApp.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage(MenuPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}