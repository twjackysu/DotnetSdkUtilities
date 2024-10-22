[Back](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/README.md)

# TypeExtensions

Do you want to check if a type implements a generic interface?

```csharp
using DotnetSdkUtilities.Extensions.TypeExtensions;
```

then
```csharp
bool isImplementIEquatable = typeof(YourClass).IsImplementGenericInterface(typeof(IEquatable<>));
```