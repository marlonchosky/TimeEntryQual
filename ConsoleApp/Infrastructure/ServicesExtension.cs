using ConsoleApp.HostedServices;
using Domain.AdditionalLogics;
using Domain.BusinessCaseLogics;
using Domain.ConfigurationClasses;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.HttpClientImpl;

namespace ConsoleApp.Infrastructure;

internal static class ServicesExtension {
    internal static void AddTimeEntryRecordLogic(this IServiceCollection services,
        HostApplicationBuilder builder) {
        _ = builder.Configuration.AddJsonFile("datesToExcludeSettings.json");

        _ = services.AddHostedService<ClockInAndClockOutHostedWorker>();
        _ = services.AddSingleton<IWorkShiftRecorder, WorkShiftRecorder>();

        _ = services.AddSingleton<ITimeEntryVerifier, TimeEntryVerifier>()
            .AddSingleton<IExcludedDatesVerifier, ExcludedDatesVerifier>()
            .AddSingleton<IHistoricTimeEntriesAndUserSettingsVerifier, HistoricTimeEntriesAndUserSettingsVerifier>()
            .AddSingleton<ILatestTimeEntryRetriever, LatestTimeEntryRetriever>();

        _ = services.AddSingleton<IExcludedDatesRepository, ConfigExcludedDatesRepository>()
            .AddSingleton<IHistoricalRecordRepository, HistoricalRecordRepository>()
            .AddSingleton<ITimeEntryRecorderRepository, ShiftTimeRecorderRepository>();

        _ = services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        _ = services.AddSingleton<IHtmlToRecordHistoryTransformer, HtmlToRecordHistoryTransformer>();

        _ = services.Configure<RefreshRecordHistoryOptions>(builder.Configuration.GetSection("RefreshRecordHistory"));
        _ = services.Configure<ShiftTimeRecordingInformationOptions>(builder.Configuration.GetSection("ShiftTimeRecordingInformation"));
        _ = services.Configure<UserDataOptions>(builder.Configuration.GetSection("UserData"));
        _ = services.Configure<DatesToExcludeOptions>(builder.Configuration.GetSection("datesToExclude"));
    }
}
