using Moq;
using CustomerApp.Features.Favorites;
using SodaShared.Models;
using CustomerApp.Shared.Services;
using CustomerApp.Features.Login;
using CustomerApp.Features.Menu;

namespace CustomerAppTests;
[TestFixture]
public class FavoritesPageViewModelTests
{
    private FavoritesPageViewModel viewModel;
    private Mock<IUserService> userServiceMock;
    private Mock<INavigationService> navigationServiceMock;
    private Mock<IFavoritesService> favoritesServiceMock;

    [SetUp]
    public void SetUp()
    {
        userServiceMock = new Mock<IUserService>();
        navigationServiceMock = new Mock<INavigationService>();
        favoritesServiceMock = new Mock<IFavoritesService>();

        viewModel = new FavoritesPageViewModel(userServiceMock.Object, navigationServiceMock.Object, favoritesServiceMock.Object);
    }

    [Test]
    public async Task Initialize_WhenNotLoggedIn_NavigatesToLoginPage()
    {
        // Arrange
        userServiceMock.Setup(x => x.IsLoggedIn()).Returns(false);

        // Act
        await viewModel.Initialize();

        // Assert
        navigationServiceMock.Verify(x => x.ClearStack(), Times.Once);
        navigationServiceMock.Verify(x => x.GoTo(nameof(LoginPage)), Times.Once);
        favoritesServiceMock.Verify(x => x.GetFavorites(), Times.Never);
    }

    [Test]
    public async Task Initialize_WhenLoggedIn_GetsFavoritesFromService()
    {
        // Arrange
        userServiceMock.Setup(x => x.IsLoggedIn()).Returns(true);
        var expectedFavorites = new List<Product>();
        favoritesServiceMock.Setup(x => x.GetFavorites()).ReturnsAsync(expectedFavorites);

        // Act
        await viewModel.Initialize();

        // Assert
        navigationServiceMock.Verify(x => x.ClearStack(), Times.Never);
        navigationServiceMock.Verify(x => x.GoTo(nameof(LoginPage)), Times.Never);
        favoritesServiceMock.Verify(x => x.GetFavorites(), Times.Once);
        Assert.AreEqual(expectedFavorites, viewModel.Favorites);
    }

    [Test]
    public async Task ToggleFavorite_CallsToggleFavoriteOnServiceAndReinitializes()
    {
        // Arrange
        userServiceMock.Setup(x => x.IsLoggedIn()).Returns(true);
        var product = new Product { Id = 1 };
        favoritesServiceMock.Setup(x => x.ToggleFavorite(product.Id)).Returns(Task.CompletedTask);
        favoritesServiceMock.Setup(x => x.GetFavorites()).ReturnsAsync(new List<Product>());

        // Act
        await viewModel.ToggleFavorite(product);

        // Assert
        favoritesServiceMock.Verify(x => x.ToggleFavorite(product.Id), Times.Once);
        favoritesServiceMock.Verify(x => x.GetFavorites(), Times.Once);
    }

    [Test]
    public async Task GoToDetails_NavigatesToProductDetailPage()
    {
        // Arrange
        var product = new Product { Id = 1 };
        var expectedRoute = $"///MenuPage/{nameof(ProductDetailPage)}";
        var expectedParameters = new Dictionary<string, object> { ["Product"] = product };

        // Act
        await viewModel.GoToDetails(product);

        // Assert
        navigationServiceMock.Verify(x => x.GoTo(expectedRoute, expectedParameters), Times.Once);
    }
}
