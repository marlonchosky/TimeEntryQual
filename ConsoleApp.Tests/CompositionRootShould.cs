using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Infrastructure;
using ConsoleApp.HostedServices;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp.Tests;

public class CompositionRootShould {
    [Fact]
    public void GetHostedService() {
        var builder = Host.CreateApplicationBuilder([]);
        builder.Services.AddTimeEntryRecordLogic(builder);

        var provider = builder.Services.BuildServiceProvider();
        var worker = provider.GetServices<IHostedService>().OfType<ClockInAndClockOutHostedWorker>().Single();
        Assert.NotNull(worker);
    }
}
