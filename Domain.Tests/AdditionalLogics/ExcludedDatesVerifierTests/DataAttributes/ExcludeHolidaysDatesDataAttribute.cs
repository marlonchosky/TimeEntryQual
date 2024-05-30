using Domain.DataContainers;
using System.Reflection;
using Xunit.Sdk;

namespace Domain.Tests.AdditionalLogics.ExcludedDatesVerifierTests.DataAttributes;

internal class ExcludeHolidaysDatesDataAttribute : DataAttribute {
    public override IEnumerable<object[]> GetData(MethodInfo testMethod) {
        yield return new object[] {
            new FullExcludedDates([], [new ExcludedDateWithDescription() { Date = new DateOnly(2024, 5, 10)}]),
            new DateOnly(2024, 5, 10)
        };
        yield return new object[] {
            new FullExcludedDates([], [new ExcludedDateWithDescription() { Date = new DateOnly(2024, 5, 11)}]),
            new DateOnly(2024, 5, 11)
        };
    }
}
