using Domain.AdditionalLogics;
using Domain.ConfigurationClasses;
using Domain.DataContainers;
using Domain.Exceptions;
using Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.DataAttributes;
using Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.FakeImpl;
using Microsoft.Extensions.Options;
using Moq;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests;
public class SUTShould {
    [Theory]
    [IncludingNumberOfDaysExceededTimeData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/DateIsPreviousToTheLatestTimeEntry.json")]
    public async Task ThrowExceptionWhenCurrentDateIsPreviousToTheLatestTimeEntry(
        DateTime currentDateTime, LatestTimeEntry latestTimeEntry, int numberOfDaysExceeded) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            Mock.Of<IOptionsMonitor<UserDataOptions>>()
        );

        var exception = await Assert.ThrowsAsync<CurrentDateCannotBePreviousToTheLatestClockDateException>(
            sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings
        );

        Assert.Equal(numberOfDaysExceeded, exception.NumberOfDays);
    }

    [Theory]
    [CompleteInformationData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/SameDateButTimeIsBeforeTheValidTimeSlot.json")]
    internal async Task InvalidateForSameDateButTimeIsBeforeTheValidTimeSlot(LatestTimeEntry latestTimeEntry,
        DateTime currentDateTime, UserDataOptions userDataOptions) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(m => m.CurrentValue).Returns(userDataOptions);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            mockUserDataOptions.Object
        );

        var result = await sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings();
        Assert.False(result);
    }

    [Theory]
    [CompleteInformationData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/TheCurrentDateTimeIsInTheTimeSlot.json")]
    internal async Task AllowForSameDate(LatestTimeEntry latestTimeEntry,
        DateTime currentDateTime, UserDataOptions userDataOptions) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(m => m.CurrentValue).Returns(userDataOptions);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            mockUserDataOptions.Object
        );

        var result = await sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings();
        Assert.True(result);
    }

    [Theory]
    [CompleteInformationWithTimeExceededData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/TimeExceedsValidTimeSlotForSameDate.json")]
    internal async Task ThrowExceptionWhenTimeExceedsValidTimeSlotForSameDate(LatestTimeEntry latestTimeEntry,
        DateTime currentDateTime, UserDataOptions userDataOptions, TimeSpan timeSpanExceeded) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(m => m.CurrentValue).Returns(userDataOptions);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            mockUserDataOptions.Object
        );

        var exception = await Assert.ThrowsAsync<TimeExceededForClockOutException>(
            sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings
        );

        Assert.Equal(timeSpanExceeded, exception.TimeSpan);
    }

    [Theory]
    [IncludingNumberOfDaysExceededTimeData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/DateExceededForMoreThanOneDay.json")]
    internal async Task ThrowExceptionWhenDateExceedsValidTimeSlotForMoreThanOneDay(
        DateTime currentDateTime, LatestTimeEntry latestClockRecord, int numberOfDaysExceeded) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestClockRecord);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            Mock.Of<IOptionsMonitor<UserDataOptions>>()
        );

        var exception = await Assert.ThrowsAsync<DateExceededByMoreThanOneDayForClockInException>(
            sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings
        );
        Assert.Equal(numberOfDaysExceeded, exception.NumberOfDaysExceeded);
    }

    [Theory]
    [CompleteInformationData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/AfterDateButTimeIsBeforeTheValidTimeSlot.json")]
    internal async Task InvalidateForDateAfterCurrentDateButTimeIsBeforeTheValidTimeSlot(LatestTimeEntry latestTimeEntry,
        DateTime currentDateTime, UserDataOptions userDataOptions) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(m => m.CurrentValue).Returns(userDataOptions);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            mockUserDataOptions.Object
        );

        var result = await sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings();
        Assert.False(result);
    }

    [Theory]
    [CompleteInformationData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/DayAfterAndValidTime.json")]
    internal async Task AllowForDayAfter(LatestTimeEntry latestTimeEntry,
        DateTime currentDateTime, UserDataOptions userDataOptions) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(m => m.CurrentValue).Returns(userDataOptions);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            mockUserDataOptions.Object
        );

        var result = await sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings();
        Assert.True(result);
    }

    [Theory]
    [CompleteInformationWithTimeExceededData($"{nameof(AdditionalLogics)}/" +
        $"{nameof(HistoricTimeEntriesAndUserSettingsVerifierTests)}/Data/TimeExceedsValidTimeSlotForDayAfter.json")]
    internal async Task ThrowExceptionWhenTimeExceedsValidTimeSlotForADayAfter(LatestTimeEntry latestTimeEntry,
        DateTime currentDateTime, UserDataOptions userDataOptions, TimeSpan timeSpanExceeded) {
        var latestTimeEntryRetrieverMock = new Mock<ILatestTimeEntryRetriever>();
        _ = latestTimeEntryRetrieverMock.Setup(l => l.GetLatestTimeEntry()).ReturnsAsync(latestTimeEntry);

        var mockUserDataOptions = new Mock<IOptionsMonitor<UserDataOptions>>();
        _ = mockUserDataOptions.Setup(m => m.CurrentValue).Returns(userDataOptions);

        var sut = new HistoricTimeEntriesAndUserSettingsVerifier(
            new FixDateTimeProvider(currentDateTime),
            latestTimeEntryRetrieverMock.Object,
            mockUserDataOptions.Object
        );

        var exception = await Assert.ThrowsAsync<TimeExceededForClockInException>(
            sut.IsAValidTimeEntryBasedOnHistoricTimeEntriesAndUserSettings
        );

        Assert.Equal(timeSpanExceeded, exception.TimeSpan);
    }
}
