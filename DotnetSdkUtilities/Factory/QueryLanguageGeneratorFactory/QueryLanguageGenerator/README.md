[Back to QueryLanguageGeneratorFactory](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/DotnetSdkUtilities/Factory/QueryLanguageGeneratorFactory/README.md)

# QueryLanguageGenerator

## Extending QueryLanguageGenerator and QueryLanguageType

To extend the `QueryLanguageGenerator` and add your own `QueryLanguageType` using this library, follow these steps:

1. **Create Your Query Language Generator Class**

   Implement the `IQueryLanguageGenerator` interface for your custom query language generator.

   ```csharp
   using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
   using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

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

2. **Define Your Query Language Type**

   Define a constant for your new query language type.

   ```csharp
   namespace YourNamespace
   {
       public static class CustomQueryLanguageTypes
       {
           public const string MyCustomLanguage = "MyCustomLanguage"; // Add your custom language type
       }
   }
   ```

3. **Register Your Query Language Generator**

   Register your custom query language generator with the `QueryLanguageGeneratorFactory`.

   ```csharp
   using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory;
   using YourNamespace;

   class Program
   {
       static void Main(string[] args)
       {
           IQueryLanguageGeneratorFactory factory = new QueryLanguageGeneratorFactory();
           factory.RegisterGenerator(CustomQueryLanguageTypes.MyCustomLanguage, () => new MyCustomGenerator());

           var generator = factory.CreateGenerator(CustomQueryLanguageTypes.MyCustomLanguage);
           // Use the generator
           // ...existing code...
       }
   }
   ```

By following these steps, you can extend the `QueryLanguageGenerator` and add new `QueryLanguageType` without modifying the core library code.


