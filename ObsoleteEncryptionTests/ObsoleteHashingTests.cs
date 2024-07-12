using NUnit.Framework;

namespace ObsoleteEncryptionTests;

public class ObsoleteHashingTests
{
    private const string TestText = "The cat sat on the mat";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_GetMd5HashOfText_Returns_Hash_All_Be_It_With_Obsolete_Method()
    {
        var hasher = new ObsoleteEncryptionModel.ObsoleteHasher();
        var hashed = hasher.GetMd5HashOfText(TestText);
        Assert.That(hashed, Is.Not.Null);
    }
}