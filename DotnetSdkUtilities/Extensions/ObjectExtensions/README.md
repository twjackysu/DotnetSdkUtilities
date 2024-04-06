[Back](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/README.md)

# ObjectExtensions

### GetValue & SetValue

You can get or set property from an object by a string path.
```csharp
using DotnetSdkUtilities.ObjectExtensions;
```

try this and see how to write this code
```csharp
//Get yourObject.a1[0].b1[1].c1 value
Console.WriteLine(yourObject.GetValue("a1[0].b1[1].c1"));
//Set yourObject.a1[0].b1[1].c1 to "WTF"
yourObject.SetValue("a1[0].b1[1].c1", "WTF");
```

### ToCacheKey
Get the Cache key (string) from any object
```csharp
var obj = new { Name = "John", Age = 30 };
string cacheKey = obj.ToCacheKey();
```
For complex cases, please refer to the [test case](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/TestCase/ObjectExtensionsTest.cs)