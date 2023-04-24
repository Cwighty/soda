using CustomerApp.Features.Favorites;
using CustomerApp.Features.Menu;
using CustomerApp.Shared.Services;
using Moq;
using SodaShared.Models;

namespace CustomerAppTests;
[TestFixture]
public class FeaturePageViewModelTests
{
    private Mock<IProductService> productServiceMock;
    private Mock<INavigationService> navigationServiceMock;
    private FeaturePageViewModel viewModel;

    [SetUp]
    public void SetUp()
    {
        productServiceMock = new Mock<IProductService>();
        navigationServiceMock = new Mock<INavigationService>();
        viewModel = new FeaturePageViewModel(productServiceMock.Object, navigationServiceMock.Object);
    }

    [Test]
    public async Task Initialize_WhenCalled_ShouldSetCategorizedProducts()
    {
        // Arrange
        var expectedProducts = new List<Category> { new Category(), new Category() };
        productServiceMock.Setup(x => x.GetCategorizedProducts()).ReturnsAsync(expectedProducts);

        // Act
        await viewModel.Initialize();

        // Assert
        Assert.AreEqual(expectedProducts, viewModel.CategorizedProducts);
    }

    [Test]
    public async Task Initialize_AfterCalled_ShouldSetIsBusyFalse()
    {
        // Arrange
        productServiceMock.Setup(x => x.GetCategorizedProducts()).ReturnsAsync(new List<Category>());

        // Act
        await viewModel.Initialize();

        // Assert
        Assert.IsFalse(viewModel.IsBusy);
    }

    [Test]
    public async Task NavigateToDetail_WhenCalled_ShouldGoToProductDetailPage()
    {
        // Arrange
        var product = new Product();

        // Act
        await viewModel.NavigateToDetail(product);

        // Assert
        navigationServiceMock.Verify(x => x.GoTo(nameof(ProductDetailPage), It.Is<Dictionary<string, object>>(d => d["Product"] == product)));
    }

    [Test]
    public async Task GoToFavorites_WhenCalled_ShouldGoToFavoritesPage()
    {
        // Act
        await viewModel.GoToFavorites();

        // Assert
        navigationServiceMock.Verify(x => x.GoTo(nameof(FavoritesPage)));
    }

    [Test]
    public async Task GoToProfile_WhenCalled_ShouldGoToLoginPage()
    {
        // Act
        await viewModel.GoToProfile();

        // Assert
        navigationServiceMock.Verify(x => x.GoTo("/LoginPage"));
    }
}
