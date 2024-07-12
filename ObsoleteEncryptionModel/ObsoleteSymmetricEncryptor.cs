using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ObsoleteEncryptionModel;

/// <summary>
/// This is a class that contains obsolete symmetric encryption methods. It should never be used in a real application.
/// It can be used for testing code-analysis tools.
/// </summary>
public partial class ObsoleteSymmetricEncryptor
{
    // In a real application this would not be hard-coded here.
    private const string InputKey = "159B81FE-1234-3BE9-T5H2-156F4B5B9EA7";
    
    /// <summary>
    /// This uses an out-of-date encryption algorithm and should not be used in a real application.
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string EncryptWithRijndael(string plainText, string salt)
    {
        ArgumentNullException.ThrowIfNull(plainText);

        var rijndael = GetRijndaelManaged(salt);
        var encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);
        var ms = new MemoryStream();
        using (var csEncrypt = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }
    
    /// <summary>
    /// This uses an out-of-date encryption algorithm and should not be used in a real application.
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public string DecryptWithRijndael(string cipherText, string salt)
    {
        ArgumentNullException.ThrowIfNull(cipherText);

        if (!IsBase64String(cipherText))
            throw new Exception($"{nameof(cipherText)} is not base64 encoded");

        var rijndael = GetRijndaelManaged(salt);
        var decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);
        var cipher = Convert.FromBase64String(cipherText);

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
        return base64String.Length%4 == 0 && RegexForBase64Validation().IsMatch(base64String);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)]
    private static partial Regex RegexForBase64Validation();
}