namespace CustomerApp.Features.Profile;

public partial class ProfilePage : BasePage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}