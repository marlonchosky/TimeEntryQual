namespace Domain.AdditionalLogics;

public interface IHistoricTimeEntriesAndUserSettingsVerifier {
    /// <exception cref="Exceptions.CurrentDateCannotBePreviousToTheLatestClockDateException"></exception>
    /// <exception cref="Exceptions.TimeExceededForClockOutException"></exception>
    /// <exception cref="Exceptions.DateExceededByMoreThanOneDayForClockInException"></exception>
    /// <exception cref="Exceptions.TimeExceededForClockInException"></exception>
    Task<bool> IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings();
}
