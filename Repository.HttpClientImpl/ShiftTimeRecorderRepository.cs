using Domain.ConfigurationClasses;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Repository.HttpClientImpl;

public class ShiftTimeRecorderRepository(
    IOptionsMonitor<ShiftTimeRecordingInformationOptions> shiftTimeRecordingInformationOptions,
    IOptionsMonitor<UserDataOptions> userDataOption) : ITimeEntryRecorderRepository {

    private readonly ShiftTimeRecordingInformationOptions _shiftTimeRecordingInformationOptions = shiftTimeRecordingInformationOptions.CurrentValue;
    private readonly UserDataOptions _userDataOption = userDataOption.CurrentValue;

    public async Task Record() {
        HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("text/html")
        );
        client.DefaultRequestHeaders.AcceptLanguage.Clear();
        client.DefaultRequestHeaders.AcceptLanguage.Add(
            new StringWithQualityHeaderValue("en-US")
        );
        client.DefaultRequestHeaders.CacheControl ??= new CacheControlHeaderValue();
        client.DefaultRequestHeaders.CacheControl.MaxAge = TimeSpan.Zero;
        client.DefaultRequestHeaders.Add("Origin", this._shiftTimeRecordingInformationOptions.Origin);
        client.DefaultRequestHeaders.Referrer = new Uri(this._shiftTimeRecordingInformationOptions.Referrer);
        client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36"
        );
        HttpRequestMessage message = new(HttpMethod.Post, this._shiftTimeRecordingInformationOptions.Url);
        message.Headers.Add("Cookie", $"JSESSIONID={this._shiftTimeRecordingInformationOptions.JSONId}");

        var content = new FormUrlEncodedContent([
            new("p1", this._userDataOption.Id),
            new("p6", this._userDataOption.Id),
            new("Submit", "PUNCH"),
        ]);
        message.Content = content;

        HttpResponseMessage response;
        try {
            response = await client.SendAsync(message);
        } catch (HttpRequestException ex) when (ex.HttpRequestError == HttpRequestError.ConnectionError) {
            throw;
        }

        _ = await response.Content.ReadAsStringAsync();
    }
}
