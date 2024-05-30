using Domain.DataContainers;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

internal class RecordForLatestTimeEntryAndCurrentTime(DateOnly latestDate, TimeOnly latestTimeIn,
    TimeOnly? latestTimeOut, DateTime currentDateTime)
{
    public DateOnly LatestDate { get; } = latestDate;
    public TimeOnly LatestTimeIn { get; } = latestTimeIn;
    public TimeOnly? LatestTimeOut { get; } = latestTimeOut;
    public DateTime CurrentDateTime { get; } = currentDateTime;

    internal LatestTimeEntry ToLatestTimeEntry() => new("",
        LatestDate, LatestTimeIn, LatestTimeOut);
}
