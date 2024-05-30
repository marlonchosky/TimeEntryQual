using Domain.ConfigurationClasses;
using Domain.DataContainers;
using Microsoft.Extensions.Options;

namespace Domain.Repositories;
public class ConfigExcludedDatesRepository(
    IOptionsMonitor<DatesToExcludeOptions> daysToExcludeOptionsSnapshot)
    : IExcludedDatesRepository {

    private readonly DatesToExcludeOptions _daysToExcludeOptions = daysToExcludeOptionsSnapshot.CurrentValue;

    public Task<FullExcludedDates> GetExcludedDates() {
        var repeatedDays = this._daysToExcludeOptions.RepeatedDays.Select(Enum.Parse<DayOfWeek>).ToList();
        var excludedDates = this._daysToExcludeOptions.ExcludedDates.Select(x => new ExcludedDateWithDescription {
            Date = x.Date, 
            Description = x.Description
        }).ToList();

        return Task.FromResult(new FullExcludedDates(repeatedDays, excludedDates));
    }
}
