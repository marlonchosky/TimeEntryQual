using Domain.DataContainers;

namespace Domain.AdditionalLogics;

public interface IHtmlToRecordHistoryTransformer {
    internal LatestTimeEntry TransformAndGetLatestTimeEntry(string htmlContent);
}
