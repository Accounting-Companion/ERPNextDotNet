using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ERPNextDotNet.Core.JsonConverters;
public class BoolYesNoConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? v = reader.GetString();
        if (v == null) return false;
        return v.Contains("Yes", StringComparison.InvariantCultureIgnoreCase);
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value ? "Yes" : "No");
    }
}
