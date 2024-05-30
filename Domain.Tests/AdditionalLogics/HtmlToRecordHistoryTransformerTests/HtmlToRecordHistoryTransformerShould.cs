using Domain.AdditionalLogics;
using Domain.DataContainers;
using System.Reflection;

namespace Domain.Tests.AdditionalLogics.HtmlToRecordHistoryTransformerTests;

// TODO: remove the sensible information inside the html content.
public class HtmlToRecordHistoryTransformerShould {
    [Fact]
    public void GetClockInAndClockOutCorrectly() {
        var htmlText = File.ReadAllText($@"{nameof(AdditionalLogics)}{Path.DirectorySeparatorChar}" +
            $@"{nameof(HtmlToRecordHistoryTransformerTests)}{Path.DirectorySeparatorChar}WithTimeOut.html");

        IHtmlToRecordHistoryTransformer transformer = new HtmlToRecordHistoryTransformer();
        var recordHistory = transformer.TransformAndGetLatestTimeEntry(htmlText);

        var collaborator = typeof(LatestTimeEntry).GetField("_collaborator", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(recordHistory);
        Assert.Equal("Pena, Marlon (338)", collaborator);

        Assert.Equal(new DateOnly(2024, 03, 27), recordHistory.LatestDate);

        var latestTimeIn = typeof(LatestTimeEntry).GetField("_latestTimeIn", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(recordHistory);
        Assert.Equal(new TimeOnly(8, 43, 38), latestTimeIn);

        var latestTimeOut = typeof(LatestTimeEntry).GetField("_latestTimeOut", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(recordHistory);
        Assert.Equal(new TimeOnly(16, 56, 30), latestTimeOut);
    }

    [Fact]
    public void GetClockInWithNoClockOutCorrectly() {
        var htmlText = File.ReadAllText($@"{nameof(AdditionalLogics)}{Path.DirectorySeparatorChar}" +
            $@"{nameof(HtmlToRecordHistoryTransformerTests)}{Path.DirectorySeparatorChar}WithoutTimeOut.html");

        IHtmlToRecordHistoryTransformer transformer = new HtmlToRecordHistoryTransformer();
        var recordHistory = transformer.TransformAndGetLatestTimeEntry(htmlText);

        var collaborator = typeof(LatestTimeEntry).GetField("_collaborator", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(recordHistory);
        Assert.Equal("Pena, Marlon (338)", collaborator);

        Assert.Equal(new DateOnly(2024, 4, 1), recordHistory.LatestDate);

        var latestTimeIn = typeof(LatestTimeEntry).GetField("_latestTimeIn", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(recordHistory);
        Assert.Equal(new TimeOnly(8, 38, 13), latestTimeIn);

        var latestTimeOut = typeof(LatestTimeEntry).GetField("_latestTimeOut", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(recordHistory);
        Assert.Null(latestTimeOut);
    }
}