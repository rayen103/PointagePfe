using System.Text.Json;
using System.Text.Json.Serialization;

namespace CollectManagement.WebAPI.Common.Converters;

/// <summary>
/// Handles TimeSpan serialization / deserialization in "HH:mm" and "HH:mm:ss" formats
/// so that Angular time-picker values (e.g. "08:30") round-trip correctly.
/// Values must fit within a SQL time column (00:00:00 – 23:59:59).
/// </summary>
public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    private static readonly string[] Formats =
        [@"hh\:mm\:ss", @"h\:mm\:ss", @"hh\:mm", @"h\:mm"];

    private static readonly TimeSpan MaxSqlTime = new(0, 23, 59, 59, 999);

    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value))
            return default;

        if (!TimeSpan.TryParseExact(value, Formats, null, out var ts))
            throw new JsonException($"The value '{value}' is not a valid time. Expected format HH:mm or HH:mm:ss.");

        if (ts < TimeSpan.Zero || ts > MaxSqlTime)
            throw new JsonException($"The time value '{value}' is out of range. Must be between 00:00 and 23:59.");

        return ts;
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
