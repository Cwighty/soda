namespace CustomerApp.Features.Login;

public partial class LoginPage : BasePage
{
	public LoginPage(LoginPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}