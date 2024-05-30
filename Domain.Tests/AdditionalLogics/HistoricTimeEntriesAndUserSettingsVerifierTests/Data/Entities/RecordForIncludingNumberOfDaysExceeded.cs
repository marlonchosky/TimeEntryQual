namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

internal class RecordForIncludingNumberOfDaysExceeded(DateOnly latestDate, TimeOnly latestTimeIn,
    TimeOnly? latestTimeOut, DateTime currentDateTime, int numberOfDaysExceeded)
    : RecordForLatestTimeEntryAndCurrentTime(latestDate, latestTimeIn, latestTimeOut, currentDateTime)
{

    public int NumberOfDaysExceeded { get; } = numberOfDaysExceeded;
}
