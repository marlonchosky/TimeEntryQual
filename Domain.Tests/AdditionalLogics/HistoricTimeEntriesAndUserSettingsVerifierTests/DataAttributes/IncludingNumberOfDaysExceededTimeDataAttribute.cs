using Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.Data.Entities;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.DataAttributes;

internal class IncludingNumberOfDaysExceededTimeDataAttribute(string filePath)
    : ReadingFromJsonBaseDataAttribute<RecordForIncludingNumberOfDaysExceeded>(filePath) {
    protected override object[] GetObjects(RecordForIncludingNumberOfDaysExceeded item) => [
        item.CurrentDateTime,
        item.ToLatestTimeEntry(),
        item.NumberOfDaysExceeded
    ];
}
