namespace Domain.AdditionalLogics;

public class TimeEntryVerifier(
    IExcludedDatesVerifier excludedDatesVerifier,
    IHistoricTimeEntriesAndUserSettingsVerifier historicAndUserSettingsVerifier
) : ITimeEntryVerifier {

    /// <exception cref="Exceptions.CurrentDateCannotBePreviousToTheLatestClockDateException"></exception>
    /// <exception cref="Exceptions.TimeExceededForClockOutException"></exception>
    /// <exception cref="Exceptions.DateExceededByMoreThanOneDayForClockInException"></exception>
    /// <exception cref="Exceptions.TimeExceededForClockInException"></exception>
    public async Task<bool> ShouldTimeEntryBeRecordedNow() =>
        !await excludedDatesVerifier.isTodayAnExcludedDate()
        && await historicAndUserSettingsVerifier.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings();
}
