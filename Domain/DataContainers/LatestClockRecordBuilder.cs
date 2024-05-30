namespace Domain.DataContainers;

public class LatestClockRecordBuilder {
    internal static LatestTimeEntry Create(string collaborator, 
        DateOnly recordHistoryLatestDate, TimeOnly timeIn, TimeOnly? timeOut) =>
        new(collaborator, recordHistoryLatestDate, timeIn, timeOut);
}
