namespace ObsoleteEncryptionTests;

public class ObsoleteSymmetricEncryptorTests
{
    private const string TestSalt = "SomeSalt";
    private const string TestText = "The cat sat on the mat";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_EncryptWithRijndael_Encrypts_All_Be_It_With_Obsolete_Method()
    {
        var encrypter = new ObsoleteEncryptionModel.ObsoleteSymmetricEncryptor();
        var encrypted = encrypter.EncryptWithRijndael(TestText, TestSalt);
        var decrypted = encrypter.DecryptWithRijndael(encrypted, TestSalt);
        Assert.That(decrypted, Is.EqualTo(TestText));
    }
}