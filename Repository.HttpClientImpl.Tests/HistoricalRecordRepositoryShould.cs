using Domain.AdditionalLogics;
using Domain.ConfigurationClasses;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit.Abstractions;

namespace Repository.HttpClientImpl.Tests;

public class HistoricalRecordRepositoryShould(ITestOutputHelper output) {
    private readonly ITestOutputHelper _output = output;
    private readonly IConfigurationRoot _config =
        new ConfigurationBuilder()
            .AddUserSecrets<HistoricalRecordRepositoryShould>()
            .Build();

    [Fact]
    public async Task GetRecordHistoryAsync() {
        var refreshRecordHistoryOptions = this._config.GetSection("RefreshRecordHistory").Get<RefreshRecordHistoryOptions>()
            ?? throw new NullReferenceException("RefreshRecordHistory secret is not set up");

        var mockRefreshRecordHistoryOptions = new Mock<IOptionsMonitor<RefreshRecordHistoryOptions>>();
        _ = mockRefreshRecordHistoryOptions.Setup(o => o.CurrentValue).Returns(refreshRecordHistoryOptions);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(o => o.CurrentValue).Returns(new UserDataOptions {
            Id = "338",
            TimeFrameForAValidClockIn = new TimeSpan(),
            TimeFrameForAValidClockOut = new TimeSpan(),
            ValidClockInStartingTime = new TimeOnly(),
            ValidClockOutEndingTime = new TimeOnly()
        });

        var sut = new HistoricalRecordRepository(mockRefreshRecordHistoryOptions.Object,
            mockUserDataOptions.Object, new DateTimeProvider());
        var data = await sut.GetFromCurrentDate();
        this._output.WriteLine(data.Html);
        Assert.NotEmpty(data.Html);
    }
}