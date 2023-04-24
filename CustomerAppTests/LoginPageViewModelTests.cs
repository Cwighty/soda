using Moq;
using CustomerApp.Features.Login;
using CustomerApp.Shared.Services;

namespace CustomerAppTests
{
    [TestFixture]
    public class LoginPageViewModelTests
    {
        private Mock<INavigationService> _navigationService;
        private Mock<IUserService> _userService;
        private LoginPageViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _navigationService = new Mock<INavigationService>();
            _userService = new Mock<IUserService>();
            _viewModel = new LoginPageViewModel(_navigationService.Object, _userService.Object);
        }

        [Test]
        public async Task Login_ValidCredentials_NavigatesToPreviousPage()
        {
            // Arrange
            _viewModel.Email = "test@example.com";
            _viewModel.Password = "password";

            // Act
            await _viewModel.Login();

            // Assert
            _userService.Verify(u => u.Login(_viewModel.Email, _viewModel.Password), Times.Once);
            _navigationService.Verify(n => n.GoTo(".."), Times.Once);
        }

        [Test]
        public async Task GoToRegister_NavigatesToRegisterPage()
        {
            // Act
            await _viewModel.GoToRegister();

            // Assert
            _navigationService.Verify(n => n.GoTo(nameof(RegisterPage)), Times.Once);
        }
    }
}

