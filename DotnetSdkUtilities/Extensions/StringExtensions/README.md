[Back](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/README.md)

# StringExtensions

You can simply encrypt and decrypt string or byte with a key. (Symmetric encryption)
```csharp
using DotnetSdkUtilities.Extensions.StringExtensions;
```

try this and see how to write this code
```csharp
var myData = "This is confidential data.";
var key = "#248GGVNvMvz";
var protectedValue = myData.Encrypt(key);
var unprotectedValue = protectedValue.Decrypt(key);
var protectedValueStr =  Convert.ToBase64String(protectedValue);
Console.WriteLine(protectedValueStr);
Console.WriteLine(unprotectedValue);
```