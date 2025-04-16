using System.Security.Claims;
using System.Text.Json;

namespace BlazorWebRtc_Client.Extensions;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseJwtToClaim(string jwt)
    {
        List<Claim> claims = new();
        String payload = jwt.Split(".")[1]; // Payload Data
        Byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        claims.AddRange(keyValuePairs!.Select(c => new Claim(c.Key, c.Value.ToString()!)));
        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}