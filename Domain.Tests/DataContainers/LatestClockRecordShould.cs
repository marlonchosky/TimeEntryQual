using Domain.DataContainers;

namespace Domain.Tests.DataContainers;

public class LatestClockRecordShould {
    [Fact]
    public void ReturnCorrectlyIfTheWholeDayHasBeenRecorded() {
        var sut = new LatestTimeEntry("", new DateOnly(), new TimeOnly(8, 10), new TimeOnly(18, 25));
        Assert.True(sut.HasTheWholeDayBeenRecorded);
    }

    [Fact]
    public void ReturnCorrectlyIfTheWholeDayHasNotBeenRecorded() {
        var sut = new LatestTimeEntry("", new DateOnly(), new TimeOnly(8, 10), null);
        Assert.False(sut.HasTheWholeDayBeenRecorded);
    }
}
