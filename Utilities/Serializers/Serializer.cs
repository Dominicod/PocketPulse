using System.Text.Json;

namespace Utilities.Serializers;

public static class Serializer
{
    public static string SerializeError(object obj)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        return JsonSerializer.Serialize(obj, options);
    }
}