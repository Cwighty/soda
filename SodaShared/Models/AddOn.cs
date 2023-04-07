namespace SodaShared.Models;

public class AddOn
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public AddOnType AddOnType { get; set; }
}