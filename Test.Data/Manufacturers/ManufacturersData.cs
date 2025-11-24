using Domain.Manufacturers;

namespace Test.Data.Manufacturers;

public static class ManufacturersData
{
    public static Manufacturer FirstTestManufacturer()
        => Manufacturer.New(ManufacturerId.New(), "LG", "South Korea");

    public static Manufacturer SecondTestManufacturer()
        => Manufacturer.New(ManufacturerId.New(), "Samsung", "South Korea");
}
