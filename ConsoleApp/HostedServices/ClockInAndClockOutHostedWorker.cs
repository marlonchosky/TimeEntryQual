using Domain.BusinessCaseLogics;

namespace ConsoleApp.HostedServices;

internal class ClockInAndClockOutHostedWorker(
    IWorkShiftRecorder recorder,
    ILogger<ClockInAndClockOutHostedWorker> logger
) : BackgroundService {

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            await this.ExecuteMainLogic();

            var timeToWait = TimeSpan.FromMinutes(new Random().Next(10, 30));
            var nextExecutionTime = DateTime.Now + timeToWait;

            logger.LogInformation("Next execution is at {NextExecutionTime:ddd MMMM hh:mm:ss}", nextExecutionTime);
            await Task.Delay(timeToWait, stoppingToken);
        }
    }

    private async Task ExecuteMainLogic() {
        try {
            await recorder.TryToRecordTimeEntry();
        } catch (Exception) { }
    }
}
