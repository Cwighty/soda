using CustomerApp.Shared.Models;
using CustomerApp.Shared.Services;
using FluentAssertions;
using Moq;

namespace CustomerAppTests;

public class CacheServiceTests
{

    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public void Test1()
    // {
    //     Assert.Pass();
    // }

    // [Test]
    // public void TestCreateCachedProductService()
    // {
    //     var mockProductService = new Mock<IProductService>();
    //     var mockCache = new Mock<ICacheService>();
    //     var cacheService = new CacheProductService(mockProductService.Object, mockCache.Object);
    // }

    // [Test]
    // public async Task TestGetProductsEmpty()
    // {
    //     var mockProductService = new Mock<IProductService>();
    //     var cacheService = new CacheProductService(mockProductService.Object);
    //     var products = await cacheService.GetProducts();
    //     Assert.AreEqual(0, products.Count);
    // }

    [Test]
    public async Task TestGetProductsFromRemote()
    {
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product>());
        var mockCache = new Mock<ICacheService>();

        var cacheService = new CacheProductService(mockProductService.Object, mockCache.Object);
        var products = await cacheService.GetProducts();
        products.Count.Should().Be(0);
    }

    //[Test]
    //public async Task TestGetOneProductFromRemote()
    //{
    //    var mockProductService = new Mock<IProductService>();
    //    mockProductService.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Test" } });
    //    var mockCache = new Mock<ICacheService>();

    //    var cacheService = new CacheProductService(mockProductService.Object, mockCache.Object);
    //    var products = await cacheService.GetProducts();
    //    Assert.AreEqual(1, products.Count);
    //}

    [Test]
    public async Task TestGetProductsFromRemoteIfNotCached()
    {
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Test" } });
        var mockCache = new Mock<ICacheService>();

        var cacheService = new CacheProductService(mockProductService.Object, mockCache.Object);
        var products = await cacheService.GetProducts();

        products.Count.Should().Be(1);
    }

    [Test]
    public async Task TestGetProductsFromCache()
    {
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProducts()).ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Test" } });
        
        var mockCache = new Mock<ICacheService>();
        var cachedProducts = new List<Product>();
        mockCache.Setup(x => x.Add<List<Product>>(It.IsAny<string>(), It.IsAny<List<Product>>())).Callback<string, List<Product>>((key, value) => cachedProducts = value);
        mockCache.Setup(x => x.Get<List<Product>>(It.IsAny<string>())).Returns(cachedProducts);

        var cacheService = new CacheProductService(mockProductService.Object, mockCache.Object);
        var products = await cacheService.GetProducts();

        products.Count.Should().Be(1);
        products.First().Name.Should().Be("Test");
    }

}