using Domain.DataContainers;
using Domain.Repositories;

namespace Domain.AdditionalLogics;

public class LatestTimeEntryRetriever(
    IHistoricalRecordRepository historicalRecordRepository,
    IHtmlToRecordHistoryTransformer htmlTransformer
) : ILatestTimeEntryRetriever {
    public async Task<LatestTimeEntry?> GetLatestTimeEntry() {
        var historicalContentAsHtml = await historicalRecordRepository.GetFromCurrentDate();
        return htmlTransformer.TransformAndGetLatestTimeEntry(historicalContentAsHtml.Html);
    }
}
