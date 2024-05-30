using Domain.AdditionalLogics;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.FakeImpl;

public class FixDateTimeProvider(DateTime dateTime) : IDateTimeProvider {
    public TimeOnly CurrentTime => throw new NotImplementedException();

    public DateOnly CurrentDate => DateOnly.FromDateTime(this.CurrentDateTime);

    public DateTime CurrentDateTime { get; } = dateTime;

    public Task<bool> IsTodayAnExcludedDateAsync() => throw new NotImplementedException();
}
