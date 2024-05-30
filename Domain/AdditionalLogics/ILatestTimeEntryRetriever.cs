using Domain.DataContainers;

namespace Domain.AdditionalLogics;

public interface ILatestTimeEntryRetriever {
    Task<LatestTimeEntry?> GetLatestTimeEntry();
}
