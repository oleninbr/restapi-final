using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.Manufacturers;

namespace Test.Data.Conditioners;

public static class ConditionersData
{
    public static Conditioner FirstTestConditioner()
        => Conditioner.New(
            ConditionerId.New(),
            "LG ArtCool",
            "AC-120",
            "SN12345",
            "Office A1",
            DateTime.UtcNow,
            new ConditionerStatusId(Guid.NewGuid()),
            new ConditionerTypeId(Guid.NewGuid()),
            new ManufacturerId(Guid.NewGuid()));

    public static Conditioner SecondTestConditioner()
        => Conditioner.New(
            ConditionerId.New(),
            "Samsung WindFree",
            "WF-300",
            "SN67890",
            "Office B2",
            DateTime.UtcNow,
            new ConditionerStatusId(Guid.NewGuid()),
            new ConditionerTypeId(Guid.NewGuid()),
            new ManufacturerId(Guid.NewGuid()));
}
