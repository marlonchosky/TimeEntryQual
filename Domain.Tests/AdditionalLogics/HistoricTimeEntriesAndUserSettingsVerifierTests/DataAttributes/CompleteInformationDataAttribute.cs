using Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.DataAttributes;

internal class CompleteInformationDataAttribute(string filePath)
    : ReadingFromJsonBaseDataAttribute<RecordComplete>(filePath) {
    protected override object[] GetObjects(RecordComplete item) => [
        item.ToLatestTimeEntry(),
        item.CurrentDateTime,
        item.ToUserDataOptions()
    ];
}
