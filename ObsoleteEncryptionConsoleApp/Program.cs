using System;
using ObsoleteEncryptionModel;

Console.WriteLine("Starting App");

var hasher = new ObsoleteHasher();
var md5Hash = hasher.GetMd5HashOfText("Text to hash");
Console.WriteLine($"{nameof(md5Hash)}: {md5Hash}");

Console.WriteLine("Completed App");