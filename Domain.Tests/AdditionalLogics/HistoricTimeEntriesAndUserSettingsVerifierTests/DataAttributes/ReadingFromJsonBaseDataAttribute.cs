using System.Reflection;
using System.Text.Json;
using Xunit.Sdk;

namespace Domain.Tests.AdditionalLogics.HistoricTimeEntriesAndUserSettingsVerifierTests.DataAttributes;

internal abstract class ReadingFromJsonBaseDataAttribute<T>(string filePath) : DataAttribute where T : notnull {
    public override IEnumerable<object[]> GetData(MethodInfo testMethod) {
        var path = Path.IsPathRooted(filePath)
            ? filePath
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);
        if (!File.Exists(path))
            throw new ArgumentException($"Could not find file at path: {path}");

        var fileData = File.ReadAllText(filePath);
        var fullData = JsonSerializer.Deserialize<List<T>>(fileData, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        }) ?? throw new NullReferenceException("The file is empty");

        foreach (var item in fullData)
            yield return this.GetObjects(item);
    }

    protected abstract object[] GetObjects(T item);
}
