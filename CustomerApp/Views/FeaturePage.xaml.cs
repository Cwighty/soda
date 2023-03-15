namespace CustomerApp.Views;

public partial class FeaturePage : BasePage
{
	public FeaturePage(FeaturePageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}