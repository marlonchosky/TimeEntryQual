namespace Domain.ConfigurationClasses;

public class UserDataOptions {
    public required string Id { get; set; }
    public required TimeOnly ValidClockInStartingTime { get; set; }
    public required TimeSpan TimeFrameForAValidClockIn { get; set; }
    public required TimeOnly ValidClockOutEndingTime { get; set; }
    public required TimeSpan TimeFrameForAValidClockOut { get; set; }
}
