namespace Domain.DataContainers;

public class LatestTimeEntry(string collaborator, DateOnly latestDate, TimeOnly latestTimeIn, TimeOnly? latestTimeOut) {
    private readonly string _collaborator = collaborator;
    private readonly TimeOnly _latestTimeIn = latestTimeIn;
    private readonly TimeOnly? _latestTimeOut = latestTimeOut;

    public DateOnly LatestDate { get; } = latestDate;
    public bool HasTheWholeDayBeenRecorded => this._latestTimeOut.HasValue;
    public bool HasLatestTimeOutBeenRecorded => this._latestTimeOut.HasValue;
}
