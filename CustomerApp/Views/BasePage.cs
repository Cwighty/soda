namespace CustomerApp.Views;

public class BasePage : ContentPage
{
    public IViewModel ViewModel => (IViewModel)BindingContext;
    public BasePage()
    {

    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await ViewModel.Initialize();
        base.OnNavigatedTo(args);
    }
    

    protected async override void OnDisappearing()
    {
        await ViewModel.Stop();
        base.OnDisappearing();
    }
}
