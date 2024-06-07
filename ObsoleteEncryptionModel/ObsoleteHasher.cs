using System.Security.Cryptography;
using System.Text;

namespace ObsoleteEncryptionModel;

/// <summary>
/// This is a class that contains obsolete hash methods. It should never be used in a real application.
/// It can be used for testing code-analysis tools.
/// </summary>
public class ObsoleteHasher
{
    /// <summary>
    /// This uses an out-of-date hashing algorithm and should not be used in a real application.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string GetMd5HashOfText(string text)
    {
        var originalBytes = Encoding.Default.GetBytes(text);
        var encodedBytes = MD5.HashData(originalBytes);

        return BitConverter.ToString(encodedBytes);
    }
}