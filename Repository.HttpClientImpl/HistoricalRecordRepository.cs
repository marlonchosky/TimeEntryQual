using Domain.AdditionalLogics;
using Domain.ConfigurationClasses;
using Domain.DataContainers;
using Domain.Exceptions;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Domain.Repositories;

public class HistoricalRecordRepository(
    IOptionsMonitor<RefreshRecordHistoryOptions> refreshRecordHistoryOptions,
    IOptionsMonitor<UserDataOptions> userDataOption,
    IDateTimeProvider dateTimeProvider) : IHistoricalRecordRepository {

    private readonly RefreshRecordHistoryOptions _refreshRecordHistoryOptions = refreshRecordHistoryOptions.CurrentValue;
    private readonly UserDataOptions _userDataOption = userDataOption.CurrentValue;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<HistoricalRecordResponse> GetFromCurrentDate() {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        client.DefaultRequestHeaders.AcceptLanguage.Clear();
        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
        client.DefaultRequestHeaders.CacheControl ??= new CacheControlHeaderValue();
        client.DefaultRequestHeaders.CacheControl.MaxAge = TimeSpan.Zero;
        client.DefaultRequestHeaders.Add("Origin", this._refreshRecordHistoryOptions.Origin);
        client.DefaultRequestHeaders.Referrer = new Uri(this._refreshRecordHistoryOptions.Referrer);
        client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
        HttpRequestMessage message = new(HttpMethod.Post, _refreshRecordHistoryOptions.Url);
        message.Headers.Add("Cookie", $"JSESSIONID={this._refreshRecordHistoryOptions.JSONId}");

        var currentTime = this._dateTimeProvider.CurrentTime;
        var content = new FormUrlEncodedContent([
            new("p1", this._userDataOption.Id),
            new("p6", "0"),
            new("Submit", "REFRESH"),
            new("Clock", currentTime.ToString("HH:mm:ss")),
        ]);
        message.Content = content;

        HttpResponseMessage response;
        try {
            response = await client.SendAsync(message);
        } catch (HttpRequestException ex) when (ex.HttpRequestError == HttpRequestError.ConnectionError) {
            throw new NoConnectionException("No connection happened when trying to get data.", ex);
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        return HistoricalRecordResponseBuilder.Create(responseContent, currentTime);
    }
}