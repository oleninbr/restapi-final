namespace Domain.Manufacturers;

public class Manufacturer
{
    public ManufacturerId Id { get; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Manufacturer(ManufacturerId id, string name, string country, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Country = country;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Manufacturer New(ManufacturerId id, string name, string country)
        => new(id, name, country, DateTime.UtcNow, null);

    public void UpdateDetails(string name, string country)
    {
        Name = name;
        Country = country;
        UpdatedAt = DateTime.UtcNow;
    }
}
