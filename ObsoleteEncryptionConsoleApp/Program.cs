using ObsoleteEncryptionModel;

Console.WriteLine("Starting MD5 hash output");

var hasher = new ObsoleteHasher();
var md5Hash = hasher.GetMd5HashOfText("Text to hash");
Console.WriteLine($"{nameof(md5Hash)}: {md5Hash}");