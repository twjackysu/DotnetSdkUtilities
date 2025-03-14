[Back to QueryLanguageGeneratorFactory](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/DotnetSdkUtilities/Factory/QueryLanguageGeneratorFactory/README.md)

# QueryLanguageStrategy


## Extending QueryLanguageStrategy

To extend the `QueryLanguageStrategy` using this library, follow these steps:

1. **Create Your Query Language Strategy Class**

   Implement the `IQueryLanguageStrategy` interface for your custom query language strategy.

   ```csharp
   using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

   namespace YourNamespace
   {
       public class MyCustomStrategy : IQueryLanguageStrategy
       {
           // Implement the methods defined in the IQueryLanguageStrategy interface
           // ...existing code...
       }
   }
   ```

2. **Use Your Query Language Strategy in Your Generator**

   Use your custom query language strategy within your custom query language generator.

   ```csharp
   using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
   using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

   namespace YourNamespace
   {
       public class MyCustomGenerator : IQueryLanguageGenerator
       {
           private readonly IQueryLanguageStrategy _strategy = new MyCustomStrategy();

           // Implement the methods defined in the IQueryLanguageGenerator interface
           // ...existing code...
       }
   }
   ```

By following these steps, you can extend the `QueryLanguageStrategy` and use it within your custom `QueryLanguageGenerator` without modifying the core library code.
