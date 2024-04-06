[Back](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/README.md)

# IEnumerableExtensions

You can get the item with the max property from any object that has IEnumerable implementation.
```csharp
using DotnetSdkUtilities.IEnumerableExtensions;
```

try
```csharp
var yourList = new List<string>() { "12345", "123", "01234", "01", "34" };
// will get "12345"
var firstMaxLenItem = yourList.FindFirstMaxPropertyItem(x => x.Length);
// will get ["12345", "01234"]
var MaxLenItems = yourList.FindMaxPropertyItems(x => x.Length).ToArray();

// will get "01"
var firstMaxLenItem = yourList.FindFirstMinPropertyItem(x => x.Length);
// will get ["01", "34"]
var MaxLenItems = yourList.FindMinPropertyItems(x => x.Length).ToArray();
```
