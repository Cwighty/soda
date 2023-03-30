namespace CustomerApp.Features.Login;

public partial class RegisterPage : BasePage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}