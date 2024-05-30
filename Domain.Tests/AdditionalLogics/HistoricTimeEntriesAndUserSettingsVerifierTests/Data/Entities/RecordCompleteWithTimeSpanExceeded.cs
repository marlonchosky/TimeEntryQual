namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

internal class RecordCompleteWithTimeSpanExceeded(DateOnly latestDate, TimeOnly latestTimeIn,
    TimeOnly? latestTimeOut, DateTime currentDateTime, TimeOnly validClockInStartingTime,
    TimeSpan timeFrameForAValidClockIn, TimeOnly validClockOutEndingTime,
    TimeSpan timeFrameForAValidClockOut, TimeSpan timeSpanExceeded)
    : RecordComplete(latestDate, latestTimeIn, latestTimeOut, currentDateTime,
        validClockInStartingTime, timeFrameForAValidClockIn, validClockOutEndingTime,
        timeFrameForAValidClockOut)
{
    public TimeSpan TimeSpanExceeded { get; } = timeSpanExceeded;
}
