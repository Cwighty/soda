using System.Text.RegularExpressions;

namespace CustomerApp.Features.Profile;
public partial class ProfileDetailsPageViewModel : BaseViewModel
{
    private readonly UserService userService;
    private readonly NavigationService navigationService;
    [ObservableProperty]
    private Customer customer;
    [ObservableProperty]
    private List<string> validationMessages = new();

    public ProfileDetailsPageViewModel(UserService userService, NavigationService navigationService)
    {
        this.userService = userService;
        this.navigationService = navigationService;
    }

    public override async Task Initialize()
    {
        Customer = await userService.GetCustomer();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task UpdateDetails()
    {
        Customer.Name = Customer.Name.Trim();
        Customer.Phone = Customer.Phone.Trim();
        if (Customer.Name == "")
        {
            ValidationMessages.Add("Please enter a name");
        }
        if (!ValidatePhoneNumber(Customer.Phone))
        {
            ValidationMessages.Add("Phone number not valid");
        }
        if (ValidationMessages.Count > 0)
        {
            // Display the validation errors to the user
            string errorMessage = "Please correct the following errors:\n";
            foreach (var validationResult in ValidationMessages)
            {
                errorMessage += $"- {validationResult}\n";
            }
            await App.Current.MainPage.DisplayAlert("Validation Error", errorMessage, "OK");
            ValidationMessages.Clear();
            return;
        }
        await userService.UpdateCustomer(Customer);
        await navigationService.GoTo($"///{nameof(ProfilePage)}");
    }

    private bool ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false;
        }

        // Use a regular expression to check if the phone number matches a valid pattern
        var regex = new Regex(@"^\+?\d{10,}$");
        return regex.IsMatch(phoneNumber);
    }

}
