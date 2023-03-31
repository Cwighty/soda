namespace CustomerApp.Features.Profile;

public partial class ProfileDetailsPage : BasePage
{
	public ProfileDetailsPage(ProfileDetailsPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}