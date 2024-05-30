namespace Domain.ConfigurationClasses;

public class DatesToExcludeOptions {
    public required IEnumerable<string> RepeatedDays { get; set; }
    public required IEnumerable<ExcludedDate> ExcludedDates { get; set; }
}
