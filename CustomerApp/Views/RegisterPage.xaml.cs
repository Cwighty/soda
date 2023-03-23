namespace CustomerApp.Views;

public partial class RegisterPage : BasePage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}