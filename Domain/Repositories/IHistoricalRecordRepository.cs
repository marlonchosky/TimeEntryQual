using Domain.DataContainers;

namespace Domain.Repositories;

public interface IHistoricalRecordRepository {
    /// <exception cref="Exceptions.NoConnectionException">
    /// Thrown when there is not possible to stablish a connection with the repository.
    /// </exception>
    public Task<HistoricalRecordResponse> GetFromCurrentDate();
}
