using Newtonsoft.Json;

namespace ObsoleteEncryptionModel;

/// <summary>
/// This is a class that contains serialize methods with a json framework that has known vulnerabilities.
/// It should never be used in a real application.
/// It can be used for testing code-analysis tools.
/// </summary>
public class ObsoleteSerializer
{
    public string SerializeWithFrameworkThatHasKnownVulnerabilities(Product product)
    {
        return JsonConvert.SerializeObject(product);
    }
}