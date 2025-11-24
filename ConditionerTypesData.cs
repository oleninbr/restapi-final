using Domain.ConditionerTypes;

namespace Tests.Data.ConditionerTypes;

public static class ConditionerTypesData
{
    public static ConditionerType FirstTestType()
        => ConditionerType.New(ConditionerTypeId.New(), "Split System");

    public static ConditionerType SecondTestType()
        => ConditionerType.New(ConditionerTypeId.New(), "Cassette");
}
