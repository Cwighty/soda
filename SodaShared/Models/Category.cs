namespace SodaShared.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<Product> Products { get; set; } = new();
}

public static class CategoryExtensions
{
    public static Category Clone(this Category category)
    {
        return new()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
            Products = category.Products
        };
    }
}
