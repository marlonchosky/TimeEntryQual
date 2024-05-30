using Domain.Repositories;

namespace Domain.AdditionalLogics;

public class ExcludedDatesVerifier(
    IExcludedDatesRepository excludedDatesRepository,
    IDateTimeProvider dateTimeProvider
) : IExcludedDatesVerifier {

    public async Task<bool> isTodayAnExcludedDate() {
        var fullExcludedDates = await excludedDatesRepository.GetExcludedDates();
        return fullExcludedDates.RepeatedDays.Contains(dateTimeProvider.CurrentDate.DayOfWeek)
            || fullExcludedDates.ExcludedDates.Select(x => x.Date).Contains(dateTimeProvider.CurrentDate);
    }
}
