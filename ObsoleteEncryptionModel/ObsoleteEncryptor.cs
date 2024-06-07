using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ObsoleteEncryptionModel;

/// <summary>
/// This is a class that contains obsolete encryption methods. It should never be used in a real application.
/// It can be used for testing code-analysis tools.
/// </summary>
public partial class ObsoleteEncryptor
{
    private const string InputKey = "159B81FE-1234-3BE9-T5H2-156F4B5B9EA7";
    
    /// <summary>
    /// This uses an out-of-date encryption algorithm and should not be used in a real application.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string EncryptWithRijndael(string text, string salt)
    {
        ArgumentNullException.ThrowIfNull(text);

        var rijndael = GetRijndaelManaged(salt);
        var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);
        var ms = new MemoryStream();
        using (var csEncrypt = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(text);
        }

        return Convert.ToBase64String(ms.ToArray());
    }
    
    /// <summary>
    /// This uses an out-of-date encryption algorithm and should not be used in a real application.
    /// </summary>
    /// <param name="encryptedText"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public string DecryptWithRijndael(string encryptedText, string salt)
    {
        ArgumentNullException.ThrowIfNull(encryptedText);

        if (!IsBase64String(encryptedText))
            throw new Exception($"{nameof(encryptedText)} is not base64 encoded");

        var rijndael = GetRijndaelManaged(salt);
        var decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);
        var cipher = Convert.FromBase64String(encryptedText);

        using var ms = new MemoryStream(cipher);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
    
    private static RijndaelManaged GetRijndaelManaged(string salt)
    {
        ArgumentNullException.ThrowIfNull(salt);
        var saltBytes = Encoding.ASCII.GetBytes(salt);
        var key = new Rfc2898DeriveBytes(InputKey, saltBytes);

        var rijndael = new RijndaelManaged();
        rijndael.Key = key.GetBytes(rijndael.KeySize / 8);
        rijndael.IV = key.GetBytes(rijndael.BlockSize / 8);

        return rijndael;
    }
    
    private static bool IsBase64String(string base64String)
    {
        base64String = base64String.Trim();
        return base64String.Length%4 == 0 && MyRegex().IsMatch(base64String);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)]
    private static partial Regex MyRegex();
}