using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Features.Cart;

public partial class PaymentPageViewModel : BaseViewModel
{
    private string cardNumber;
    private DateTime expirationDate = DateTime.Now;
    private string cVV;
    private string cardholderName;
    private readonly NavigationService navigationService;

    [Display(Name = "Card Number")]
    [Required(ErrorMessage = "Please enter a valid credit card number.")]
    [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9]{2})[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = "Please enter a valid credit card number.")]
    public string CardNumber
    {
        get => cardNumber;
        set => SetProperty(ref cardNumber, value);
    }

    [Display(Name = "Expiration Date")]
    public DateTime ExpirationDate
    {
        get => expirationDate;
        set => SetProperty(ref expirationDate, value);
    }

    [Display(Name = "CVV")]
    [Required(ErrorMessage = "Please enter a valid CVV number.")]
    [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "Please enter a valid CVV number.")]
    public string CVV
    {
        get => cVV;
        set => SetProperty(ref cVV, value);
    }

    [Display(Name = "Cardholder Name")]
    [Required(ErrorMessage = "Please enter a valid cardholder name.")]
    public string CardholderName
    {
        get => cardholderName;
        set => SetProperty(ref cardholderName, value);
    }

    public PaymentPageViewModel(NavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task SubmitPayment()
    {
        // Validate the payment fields before submitting the payment
        var validationContext = new ValidationContext(this, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(this, validationContext, validationResults, validateAllProperties: true))
        {
            // Display the validation errors to the user
            string errorMessage = "Please correct the following errors:\n";
            foreach (var validationResult in validationResults)
            {
                errorMessage += $"- {validationResult.ErrorMessage}\n";
            }
            await App.Current.MainPage.DisplayAlert("Validation Error", errorMessage, "OK");
            return;
        }

        // TODO: Submit the payment

        await navigationService.GoTo(nameof(OrderConfirmationPage));
    }
}
