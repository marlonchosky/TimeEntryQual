namespace Domain.BusinessCaseLogics;

public interface IWorkShiftRecorder {
    public Task TryToRecordTimeEntry();
}