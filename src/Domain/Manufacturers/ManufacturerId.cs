namespace Domain.Manufacturers;

public readonly record struct ManufacturerId(Guid Value)
{
    public static ManufacturerId Empty() => new(Guid.Empty);
    public static ManufacturerId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}
