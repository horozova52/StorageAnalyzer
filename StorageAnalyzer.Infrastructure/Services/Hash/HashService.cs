
using System.Security.Cryptography;

namespace StorageAnalyzer.Infrastructure.Services.Hash;

public static class HashService
{
    public static string Sha256(string filePath)
    {
        using var sha = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hash = sha.ComputeHash(stream);
        return Convert.ToHexString(hash);       
    }
}
