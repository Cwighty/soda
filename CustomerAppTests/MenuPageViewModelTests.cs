using CustomerApp.Features.Menu;
using CustomerApp.Features.Menu.Controls;
using CustomerApp.Shared.Services;
using Moq;
using SodaShared.Models;

namespace CustomerAppTests;

[TestFixture]
public class MenuPageViewModelTests
{
    private Mock<IProductService> _productServiceMock;
    private Mock<INavigationService> _navigationServiceMock;
    private MenuPageViewModel _viewModel;

    [SetUp]
    public void Setup()
    {
        _productServiceMock = new Mock<IProductService>();
        _navigationServiceMock = new Mock<INavigationService>();
        _viewModel = new MenuPageViewModel(
            _productServiceMock.Object,
            _navigationServiceMock.Object);
    }

    [Test]
    public async Task Initialize_ShouldSetBaseTypes()
    {
        // Arrange
        var baseTypes = new List<BaseType>() { new BaseType() };
        _productServiceMock.Setup(ps => ps.GetBaseTypes()).ReturnsAsync(baseTypes);

        // Act
        await _viewModel.Initialize();

        // Assert
        Assert.AreEqual(baseTypes, _viewModel.BaseTypes);
    }

    [Test]
    public async Task Initialize_ShouldSetProductsByBase()
    {
        // Arrange
        var baseProducts = new List<BaseProduct>() { new BaseProduct("test", new List<Product>()) };
        _productServiceMock.Setup(ps => ps.GetBaseProducts()).ReturnsAsync(baseProducts);

        // Act
        await _viewModel.Initialize();

        // Assert
        Assert.AreEqual(baseProducts.First().Base, _viewModel.ProductsByBase.First().Base);
    }

    [Test]
    public async Task Initialize_ShouldSetSelectedBaseType()
    {
        // Arrange
        var baseTypes = new List<BaseType>() { new BaseType() };
        _productServiceMock.Setup(ps => ps.GetBaseTypes()).ReturnsAsync(baseTypes);

        // Act
        await _viewModel.Initialize();

        // Assert
        Assert.AreEqual(baseTypes.First(), _viewModel.SelectedBaseType);
    }

    [Test]
    public async Task Navigate_ShouldCallNavigationService()
    {
        // Arrange
        var products = new List<Product>() { new Product() };

        // Act
        await _viewModel.Navigate(products);

        // Assert
        _navigationServiceMock.Verify(
            ns => ns.GoTo(
                nameof(ProductListPage),
                It.Is<Dictionary<string, object>>(d => d.ContainsKey("Products") && d["Products"] == products)),
            Times.Once);
    }

    [Test]
    public async Task NavigateToDetail_ShouldCallNavigationService()
    {
        // Arrange
        var product = new Product();

        // Act
        await _viewModel.NavigateToDetail(product);

        // Assert
        _navigationServiceMock.Verify(
            ns => ns.GoTo(
                nameof(ProductDetailPage),
                It.Is<Dictionary<string, object>>(d => d.ContainsKey("Product") && d["Product"] == product)),
            Times.Once);
    }
}
