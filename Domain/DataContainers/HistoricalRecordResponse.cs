namespace Domain.DataContainers;

public class HistoricalRecordResponse(string html, TimeOnly queryTime) {
    public string Html { get; } = html;
    public TimeOnly QueryTime { get; } = queryTime;
}
