using System.Text.Json;
using System.Text.Json.Serialization;

namespace CollectManagement.WebAPI.Common.Converters;

/// <summary>
/// Handles TimeSpan serialization / deserialization in "HH:mm" and "HH:mm:ss" formats
/// so that Angular time-picker values (e.g. "08:30") round-trip correctly.
/// </summary>
public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return default;

        if (TimeSpan.TryParseExact(value, [@"hh\:mm\:ss", @"h\:mm\:ss", @"hh\:mm", @"h\:mm"], null, out var ts))
            return ts;

        // Fall back to standard TimeSpan.Parse (covers "c" format and ISO 8601 duration)
        return TimeSpan.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
}

public class NullableTimeSpanJsonConverter : JsonConverter<TimeSpan?>
{
    private static readonly TimeSpanJsonConverter _inner = new();

    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        // Delegate to the non-nullable converter; it already handles empty strings
        // via TimeSpan.TryParseExact / TimeSpan.Parse
        var result = _inner.Read(ref reader, typeof(TimeSpan), options);
        return result == default ? null : result;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            _inner.Write(writer, value.Value, options);
    }
}
