using System;
using NUnit.Framework;
using ObsoleteEncryptionModel;

namespace ObsoleteEncryptionTests;

public class ObsoleteEncryptorTests
{
    private const string TestSalt = "SomeSalt";
    private const string TestText = "The cat sat on the mat";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [Obsolete("Obsolete")]
    public void Test_EncryptWithRijndael_Encrypts_All_Be_It_With_Obsolete_Method()
    {
        var encrypter = new ObsoleteEncryptor();
        var encrypted = encrypter.EncryptWithRijndael(TestText, TestSalt);
        var decrypted = encrypter.DecryptWithRijndael(encrypted, TestSalt);
        Assert.AreEqual(TestText, decrypted);
    }
}