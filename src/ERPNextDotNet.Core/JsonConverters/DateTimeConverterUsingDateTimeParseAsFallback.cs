﻿using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERPNextDotNet.Core.JsonConverters;
public class DateTimeConverterUsingDateTimeParseAsFallback : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));

        if (!reader.TryGetDateTime(out DateTime value))
        {
            value = DateTime.Parse(reader.GetString()!, CultureInfo.InvariantCulture);
        }

        return value;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
    }
}
