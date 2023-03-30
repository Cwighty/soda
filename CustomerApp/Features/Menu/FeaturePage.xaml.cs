namespace CustomerApp.Features.Menu;

public partial class FeaturePage : BasePage
{
	public FeaturePage(FeaturePageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}