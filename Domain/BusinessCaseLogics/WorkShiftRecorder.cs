using Domain.AdditionalLogics;
using Domain.Repositories;

namespace Domain.BusinessCaseLogics;

public class WorkShiftRecorder(
    ITimeEntryVerifier timeEntryVerifier,
    ITimeEntryRecorderRepository timeEntryRecorderRepository
) : IWorkShiftRecorder {

    public async Task TryToRecordTimeEntry() {
        if (!await timeEntryVerifier.ShouldTimeEntryBeRecordedNow())
            return;
        await timeEntryRecorderRepository.Record();
    }
}