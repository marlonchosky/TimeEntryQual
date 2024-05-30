namespace Domain.DataContainers;

public class HistoricalRecordResponseBuilder {
    public static HistoricalRecordResponse Create(string html, TimeOnly queryTime) =>
        new(html, queryTime);
}
