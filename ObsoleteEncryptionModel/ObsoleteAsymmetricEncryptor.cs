using System.Security.Cryptography;
using System.Text;

namespace ObsoleteEncryptionModel;

/// <summary>
/// This is a class that contains obsolete asymmetric encryption methods. It should never be used in a real application.
/// It can be used for testing code-analysis tools.
/// </summary>
public class ObsoleteAsymmetricEncryptor
{
    private const int RsaKeySize = 1024;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="publicKey"></param>
    /// <returns></returns>
    public string EncryptWithRsa(string plainText, string publicKey)
    {
        ArgumentNullException.ThrowIfNull(plainText);
        
        var testData = Encoding.UTF8.GetBytes(plainText);
        using (var rsa = new RSACryptoServiceProvider(RsaKeySize))
        {
            try
            {
                rsa.FromXmlString(publicKey);
                var encryptedData = rsa.Encrypt(testData, true);
                var base64Encrypted = Convert.ToBase64String(encryptedData);
                return base64Encrypted;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    public string DecryptWithRsa(string cipherText, string privateKey)
    {
        ArgumentNullException.ThrowIfNull(cipherText);

        using (var rsa = new RSACryptoServiceProvider(RsaKeySize))
        {
            try
            {
                var base64Encrypted = cipherText;
                rsa.FromXmlString(privateKey);
                var resultBytes = Convert.FromBase64String(base64Encrypted);
                var decryptedBytes = rsa.Decrypt(resultBytes, true);
                var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedData;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public string EncryptWithTripleDes(string plainText, byte[] key, byte[] iv)
    {
        ArgumentNullException.ThrowIfNull(plainText);
        
        var memoryStream = new MemoryStream();
        
        var cryptoStream = new CryptoStream(memoryStream, new DESCryptoServiceProvider().CreateEncryptor(key, iv),
            CryptoStreamMode.Write);
        
        var writer = new StreamWriter(cryptoStream);
        writer.Write(plainText);
        writer.Flush();
        cryptoStream.FlushFinalBlock();
        writer.Flush();
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public string DecryptWithTripleDes(string cipherText, byte[] key, byte[] iv)
    {
        ArgumentNullException.ThrowIfNull(cipherText);
        
        var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
        var cryptoStream = new CryptoStream(memoryStream, new DESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read);
        
        var reader = new StreamReader(cryptoStream);
        return reader.ReadToEnd();
    }
}