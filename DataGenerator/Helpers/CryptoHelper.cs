using System.Security.Cryptography;

namespace DataGenerator.Helpers;

internal static class CryptoHelper
{
    public static int GetHash(string s)
    {
        var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(s));
        return BitConverter.ToInt32(bytes, 0);
    }
}
