namespace CustomerApp.Views;

public partial class ProfilePage : BasePage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}