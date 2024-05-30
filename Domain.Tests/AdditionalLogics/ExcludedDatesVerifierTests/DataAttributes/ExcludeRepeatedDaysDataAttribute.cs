using Domain.DataContainers;
using System.Reflection;
using Xunit.Sdk;

namespace Domain.Tests.AdditionalLogics.ExcludedDatesVerifierTests.DataAttributes;
internal class ExcludeRepeatedDaysDataAttribute : DataAttribute {
    public override IEnumerable<object[]> GetData(MethodInfo testMethod) {
        yield return new object[] {
            new FullExcludedDates([DayOfWeek.Sunday, DayOfWeek.Saturday], []),
            new DateOnly(2024, 5, 11)
        };
        yield return new object[] {
            new FullExcludedDates([DayOfWeek.Sunday, DayOfWeek.Saturday], []),
            new DateOnly(2024, 5, 12)
        };
    }
}
