using Domain.ConfigurationClasses;
using Domain.DataContainers;
using Domain.Exceptions;
using Microsoft.Extensions.Options;

namespace Domain.AdditionalLogics;

public class HistoricTimeEntriesAndUserSettingsVerifier(
    IDateTimeProvider dateTimeProvider,
    ILatestTimeEntryRetriever latestTimeEntryRetriever,
    IOptionsMonitor<UserDataOptions> userDataOptionsSnapshot
) : IHistoricTimeEntriesAndUserSettingsVerifier {

    private readonly UserDataOptions _userDataOptions = userDataOptionsSnapshot.CurrentValue;

    /// <exception cref="CurrentDateCannotBePreviousToTheLatestClockDateException"></exception>
    /// <exception cref="TimeExceededForClockOutException"></exception>
    /// <exception cref="DateExceededByMoreThanOneDayForClockInException"></exception>
    /// <exception cref="TimeExceededForClockInException"></exception>
    public async Task<bool> IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings() {
        var latestTimeEntry = await latestTimeEntryRetriever.GetLatestTimeEntry() ?? throw new NotImplementedException();
        var currentDate = dateTimeProvider.CurrentDate;
        var latestDateOfClockRecord = latestTimeEntry.LatestDate;
        return currentDate < latestDateOfClockRecord
            ? throw new CurrentDateCannotBePreviousToTheLatestClockDateException(
                latestDateOfClockRecord.DayNumber - currentDate.DayNumber)
        : currentDate == latestDateOfClockRecord
            ? this.ShouldItRecordForSameDateAsLatestDateRecorded(latestTimeEntry)
            : this.ShouldItRecordForDateExceededToTheLatestDateRecorded(latestTimeEntry);
    }

    private bool ShouldItRecordForSameDateAsLatestDateRecorded(LatestTimeEntry latestTimeEntry) {
        if (latestTimeEntry.HasTheWholeDayBeenRecorded)
            return false;

        var currentDateTime = dateTimeProvider.CurrentDateTime;
        var validClockOutUpperTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day)
            + this._userDataOptions.ValidClockOutEndingTime.ToTimeSpan();
        var validClockOutLowerTime = validClockOutUpperTime - this._userDataOptions.TimeFrameForAValidClockOut;

        return currentDateTime >= validClockOutLowerTime
            && (currentDateTime > validClockOutUpperTime
                ? throw new TimeExceededForClockOutException(currentDateTime - validClockOutUpperTime)
                : true
                );
    }

    private bool ShouldItRecordForDateExceededToTheLatestDateRecorded(LatestTimeEntry latestTimeEntry) {
        var numberOfDaysExceeded = dateTimeProvider.CurrentDate.DayNumber - latestTimeEntry.LatestDate.DayNumber;
        if (numberOfDaysExceeded > 1)
            throw new DateExceededByMoreThanOneDayForClockInException(numberOfDaysExceeded);

        var currentDateTime = dateTimeProvider.CurrentDateTime;
        var validClockInLowerTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day)
            + this._userDataOptions.ValidClockInStartingTime.ToTimeSpan();
        var validClockInUpperTime = validClockInLowerTime + this._userDataOptions.TimeFrameForAValidClockIn;

        return currentDateTime >= validClockInLowerTime
            && (currentDateTime > validClockInUpperTime
                ? throw new TimeExceededForClockInException(currentDateTime - validClockInUpperTime)
                : true
                );
    }
}
