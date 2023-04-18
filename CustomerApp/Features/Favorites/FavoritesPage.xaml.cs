namespace CustomerApp.Features.Favorites;

public partial class FavoritesPage : BasePage
{
	public FavoritesPage(FavoritesPageViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}