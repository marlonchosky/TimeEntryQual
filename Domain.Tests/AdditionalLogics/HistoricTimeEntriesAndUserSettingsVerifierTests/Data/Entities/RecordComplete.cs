using Domain.ConfigurationClasses;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

internal class RecordComplete(DateOnly latestDate, TimeOnly latestTimeIn,
    TimeOnly? latestTimeOut, DateTime currentDateTime,
    TimeOnly validClockInStartingTime, TimeSpan timeFrameForAValidClockIn,
    TimeOnly validClockOutEndingTime, TimeSpan timeFrameForAValidClockOut)
    : RecordForLatestTimeEntryAndCurrentTime(latestDate, latestTimeIn,
        latestTimeOut, currentDateTime) {

    public TimeOnly ValidClockInStartingTime { get; } = validClockInStartingTime;
    public TimeSpan TimeFrameForAValidClockIn { get; } = timeFrameForAValidClockIn;
    public TimeOnly ValidClockOutEndingTime { get; } = validClockOutEndingTime;
    public TimeSpan TimeFrameForAValidClockOut { get; } = timeFrameForAValidClockOut;

    internal UserDataOptions ToUserDataOptions() => new() {
        Id = "",
        ValidClockInStartingTime = this.ValidClockInStartingTime,
        TimeFrameForAValidClockIn = this.TimeFrameForAValidClockIn,
        ValidClockOutEndingTime = this.ValidClockOutEndingTime,
        TimeFrameForAValidClockOut = this.TimeFrameForAValidClockOut
    };
}
