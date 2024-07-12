using System;
using NUnit.Framework;
using ObsoleteEncryptionModel;

namespace ObsoleteEncryptionTests;

public class ObsoleteSerializerTests
{
    private Product _testProduct;
    
    [SetUp]
    public void Setup()
    {
        _testProduct = new Product
        {
            Id = 123,
            Name = "Chair",
            CreatedDate = new DateTime(2024, 01, 01)
        };
    }

    [Test]
    public void Test_SerializeWithFrameworkThatHasKnownVulnerabilities_Serializes_With_Obsolete_Json_Framework()
    {
        var serializer = new ObsoleteSerializer();
        var serializedObject = serializer.SerializeWithFrameworkThatHasKnownVulnerabilities(_testProduct);
        Assert.That(serializedObject, Is.EqualTo("{\"Id\":123,\"Name\":\"Chair\",\"CreatedDate\":\"2024-01-01T00:00:00\"}"));
    }
}