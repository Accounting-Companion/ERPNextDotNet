using ERPNextDotNet.Core.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERPNextDotNet.Tests;
public class Utils
{
    static JsonSerializerOptions? _jsonSerializerOptions;
    static JsonSerializerOptions JsonSerializerOptions { get => _jsonSerializerOptions ??= GetJsonOptions(); }

    private static JsonSerializerOptions GetJsonOptions()
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        _jsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParseAsFallback());
        _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        return _jsonSerializerOptions;
    }

    public static Task<string> ReadFromFile(string path) => File.ReadAllTextAsync(Path.Join("./FakeResponseData", $"{path}.json"));
    public static string JsonSerialize<T>(T data) => JsonSerializer.Serialize(data, JsonSerializerOptions);
}
