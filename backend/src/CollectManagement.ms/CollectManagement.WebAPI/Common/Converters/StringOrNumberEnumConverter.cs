using System.Text.Json;
using System.Text.Json.Serialization;

namespace CollectManagement.WebAPI.Common.Converters;

/// <summary>
/// Allows enum values to be provided as either strings (case-insensitive) or numbers.
/// </summary>
public class StringOrNumberEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (string.IsNullOrWhiteSpace(value))
                throw new JsonException($"The value for {typeToConvert.Name} cannot be empty.");

            if (Enum.TryParse(value, true, out T parsed) && Enum.IsDefined(typeToConvert, parsed))
                return parsed;

            throw new JsonException($"The value '{value}' is not valid for {typeToConvert.Name}.");
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            if (!reader.TryGetInt32(out var intValue))
                throw new JsonException($"The value is not a valid integer for {typeToConvert.Name}.");

            var enumValue = (T)Enum.ToObject(typeToConvert, intValue);
            if (!Enum.IsDefined(typeToConvert, enumValue))
                throw new JsonException($"The value '{intValue}' is not valid for {typeToConvert.Name}.");

            return enumValue;
        }

        throw new JsonException($"Unexpected token {reader.TokenType} when parsing {typeToConvert.Name}.");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WriteNumberValue(Convert.ToInt32(value));
}
