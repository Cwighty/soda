namespace CustomerApp.Views;

public partial class LoginPage : BasePage
{
	public LoginPage(LoginPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}