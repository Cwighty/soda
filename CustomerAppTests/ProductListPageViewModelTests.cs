using CustomerApp.Features.Menu;
using CustomerApp.Shared.Services;
using Moq;
using SodaShared.Models;

namespace CustomerAppTests;

[TestFixture]
public class ProductListPageViewModelTests
{
    private Mock<INavigationService> _navigationServiceMock;
    private ProductListPageViewModel _viewModel;

    [SetUp]
    public void Setup()
    {
        _navigationServiceMock = new Mock<INavigationService>();
        _viewModel = new ProductListPageViewModel(_navigationServiceMock.Object);
    }

    [Test]
    public async Task Initialize_WhenCalled_SetsProductsToQueryProperty()
    {
        // Arrange
        var products = new List<Product> { new Product(), new Product() };
        _viewModel.Products = products;

        // Act
        await _viewModel.Initialize();

        // Assert
        Assert.AreEqual(products, _viewModel.Products);
    }

    [Test]
    public async Task Details_WhenCalled_NavigatesToProductDetailPage()
    {
        // Arrange
        var product = new Product();

        // Act
        await _viewModel.Details(product);

        // Assert
        _navigationServiceMock.Verify(x => x.GoTo(nameof(ProductDetailPage), It.Is<Dictionary<string, object>>(d => d["Product"] == product)), Times.Once);
    }
}
