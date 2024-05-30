namespace Domain.ConfigurationClasses;

public class RefreshRecordHistoryOptions {
    public required string Url { get; set; }
    public required string Origin { get; set; }
    public required string Referrer { get; set; }
    public required string JSONId { get; set; }
}
