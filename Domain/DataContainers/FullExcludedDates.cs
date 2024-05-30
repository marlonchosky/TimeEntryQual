namespace Domain.DataContainers;
public class FullExcludedDates(IList<DayOfWeek> repeatedDays, IList<ExcludedDateWithDescription> excludedDates) {
    public IList<DayOfWeek> RepeatedDays { get; } = repeatedDays;
    public IList<ExcludedDateWithDescription> ExcludedDates { get; } = excludedDates;
}
