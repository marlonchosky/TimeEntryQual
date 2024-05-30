using Domain.AdditionalLogics;
using Domain.DataContainers;
using Domain.Repositories;
using Domain.Tests.AdditionalLogics.ExcludedDatesVerifierTests.DataAttributes;
using Moq;

namespace Domain.Tests.AdditionalLogics.ExcludedDatesVerifierTests;
public class SUTShould {
    [Theory]
    [ExcludeRepeatedDaysData]
    public async Task ExcludeRepeteadedDays(FullExcludedDates fullExcludedDates, DateOnly currentDate) {
        var excludedDatesRepositoryMock = new Mock<IExcludedDatesRepository>();
        excludedDatesRepositoryMock.Setup(e => e.GetExcludedDates())
            .ReturnsAsync(fullExcludedDates);

        var dateTimeProviderMock = new Mock<IDateTimeProvider>();
        dateTimeProviderMock.Setup(dateTimeProviderMock => dateTimeProviderMock.CurrentDate)
            .Returns(currentDate);

        var sut = new ExcludedDatesVerifier(excludedDatesRepositoryMock.Object,
            dateTimeProviderMock.Object);
        var result = await sut.isTodayAnExcludedDate();
        Assert.True(result);
    }

    [Theory]
    [ExcludeHolidaysDatesData]
    public async Task ExcludeSomeDates(FullExcludedDates fullExcludedDates, DateOnly currentDate) {
        var excludedDatesRepositoryMock = new Mock<IExcludedDatesRepository>();
        excludedDatesRepositoryMock.Setup(e => e.GetExcludedDates())
            .ReturnsAsync(fullExcludedDates);

        var dateTimeProviderMock = new Mock<IDateTimeProvider>();
        dateTimeProviderMock.Setup(dateTimeProviderMock => dateTimeProviderMock.CurrentDate)
            .Returns(currentDate);

        var sut = new ExcludedDatesVerifier(excludedDatesRepositoryMock.Object,
            dateTimeProviderMock.Object);
        var result = await sut.isTodayAnExcludedDate();
        Assert.True(result);
    }
}
