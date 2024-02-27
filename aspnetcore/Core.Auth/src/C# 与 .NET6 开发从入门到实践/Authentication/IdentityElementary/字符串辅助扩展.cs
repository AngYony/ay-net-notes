namespace OpenIddictServer;

public static class StringExtensions
{
    public static byte[] ToBytesFromBase64String(this string base64String)
    {
        return Convert.FromBase64String(base64String);
    }

    public static string ToBase64String(this byte[] value)
    {
        return Convert.ToBase64String(value);
    }
}
