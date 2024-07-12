using System.Security.Cryptography;
using System.Text;

namespace ObsoleteEncryptionTests;

public class ObsoleteAsymmetricEncryptorTests
{
    private const string TestText = "The cat sat on the mat";
    private const string RsaPublicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
    private const string RsaPrivateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";
    private byte[] _tripleDesKey;
    private byte[] _tripleDesIv;
    
    [SetUp]
    public void Setup()
    {
        var desCryptoServiceProvider = new DESCryptoServiceProvider();
        _tripleDesKey = desCryptoServiceProvider.Key;
        _tripleDesIv = desCryptoServiceProvider.IV;
    }

    [Test]
    public void Test_EncryptWithRsa_Encrypts_All_Be_It_With_Obsolete_Method()
    {
        var encrypter = new ObsoleteEncryptionModel.ObsoleteAsymmetricEncryptor();
        var encrypted = encrypter.EncryptWithRsa(TestText, RsaPublicKey);
        var decrypted = encrypter.DecryptWithRsa(encrypted, RsaPrivateKey);
        Assert.That(decrypted, Is.EqualTo(TestText));
    }
    
    [Test]
    public void Test_DecryptWithTripleDes_Encrypts_All_Be_It_With_Obsolete_Method()
    {
        var encrypter = new ObsoleteEncryptionModel.ObsoleteAsymmetricEncryptor();
        var encrypted = encrypter.EncryptWithTripleDes(TestText, _tripleDesKey, _tripleDesIv);
        var decrypted = encrypter.DecryptWithTripleDes(encrypted, _tripleDesKey, _tripleDesIv);
        Assert.That(decrypted, Is.EqualTo(TestText));
    }
}