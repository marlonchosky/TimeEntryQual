namespace Domain.Repositories;

public class FakeShiftTimeRecorderRepository : ITimeEntryRecorderRepository {
    public Task Record() => Task.CompletedTask;
}
