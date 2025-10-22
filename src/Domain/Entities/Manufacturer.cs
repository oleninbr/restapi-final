namespace Domain.Entities;

public class Manufacturer
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Manufacturer(Guid id, string name, string country, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Name = name;
        Country = country;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Manufacturer New(Guid id, string name, string country)
    {
        return new Manufacturer(id, name, country, DateTime.UtcNow, null);
    }

    public void UpdateDetails(string name, string country)
    {
        Name = name;
        Country = country;
        UpdatedAt = DateTime.UtcNow;
    }
}
