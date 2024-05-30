using Domain.DataContainers;
using HtmlAgilityPack;

namespace Domain.AdditionalLogics;

public class HtmlToRecordHistoryTransformer : IHtmlToRecordHistoryTransformer {
    LatestTimeEntry IHtmlToRecordHistoryTransformer.TransformAndGetLatestTimeEntry(string htmlContent) {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);

        var htmlTableWithRecordHistory = htmlDocument.DocumentNode.SelectSingleNode("/descendant::table[2]");
        var collaborator = GetCollaboratorData(htmlTableWithRecordHistory);
        var recordHistoryLatestDate = GetRecordHistoryLatestDate(htmlTableWithRecordHistory);
        var (timeIn, timeOut) = GetLatestTimeInAndLatestTimeOut(htmlTableWithRecordHistory);

        return LatestClockRecordBuilder.Create(collaborator, recordHistoryLatestDate, timeIn, timeOut);
    }

    private static string GetCollaboratorData(HtmlNode htmlTableWithRecordHistory) =>
        htmlTableWithRecordHistory.SelectSingleNode("./tr[2]/td[2]").InnerText.Trim();

    private static DateOnly GetRecordHistoryLatestDate(HtmlNode htmlTableWithRecordHistory) =>
        DateOnly.ParseExact(htmlTableWithRecordHistory.SelectSingleNode("./tr[2]/td[3]").InnerText.Trim()[..10], "yyyy-MM-dd");

    private static (TimeOnly, TimeOnly?) GetLatestTimeInAndLatestTimeOut(HtmlNode htmlTableWithRecordHistory) {
        var timeIn = TimeOnly.ParseExact(htmlTableWithRecordHistory.SelectSingleNode("./tr[2]/td[4]").InnerText.Trim()[..8], "HH:mm:ss");

        var textInTimeOutTag = htmlTableWithRecordHistory.SelectSingleNode("./tr[2]/td[5]").InnerText.Trim();
        TimeOnly? timeOutNode = null;
        var lenght = ..(textInTimeOutTag.Length > 7 ? 8 : textInTimeOutTag.Length);
        var couldTextBeenParsed = TimeOnly.TryParseExact(textInTimeOutTag[lenght], "HH:mm:ss", out var timeOutNodeNotNull);
        if (couldTextBeenParsed) timeOutNode = timeOutNodeNotNull;

        return (timeIn, timeOutNode);
    }
}
