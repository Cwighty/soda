namespace SodaShared.Models;

public class BaseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Size> Sizes { get; set; }
}