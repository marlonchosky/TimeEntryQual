using Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.DataAttributes;

internal class CompleteInformationWithTimeExceededDataAttribute(string filePath)
        : ReadingFromJsonBaseDataAttribute<RecordCompleteWithTimeSpanExceeded>(filePath) {
    protected override object[] GetObjects(RecordCompleteWithTimeSpanExceeded item) => [
        item.ToLatestTimeEntry(),
        item.CurrentDateTime,
        item.ToUserDataOptions(),
        item.TimeSpanExceeded
    ];
}
