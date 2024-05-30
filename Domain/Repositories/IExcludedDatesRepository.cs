
using Domain.DataContainers;

namespace Domain.Repositories;
public interface IExcludedDatesRepository {
    Task<FullExcludedDates> GetExcludedDates();
}
