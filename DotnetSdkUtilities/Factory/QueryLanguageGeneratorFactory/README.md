[Back](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/README.md)

# QueryLanguageGeneratorFactory

## How to use

```csharp
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var factory = new QueryLanguageGeneratorFactory();
        var generator = factory.CreateGenerator(QueryLanguageTypes.SQL); // or use KQL
        var source = new QuerySource { Name = "YourTable" };

        // 1. Generate summarize query
        var viewByFields = new List<IViewByField>
        {
            new ViewByField { Name = "Region" }
        };

        var filterFields = new List<IFilterField>
        {
            new FilterField { Name = "Region", SpecifiedValues = new List<string> { "Europe" } },
            new FilterField { Name = "AccountType", SpecifiedValues = new List<string> { "Partner" } }
        };

        var measurementFields = new List<IMeasurementField>
        {
            new MeasurementField
            {
                Name = "AverageARR",
                Expression = new CompositeExpression(
                    "/",
                    new SumExpression("ARR"),
                    new CountDistinctExpression("Account_ID")
                )
            }
        };

        var summarizeQuery = generator.GenerateSummarizeQuery(source, viewByFields, filterFields, measurementFields);
        Console.WriteLine("Summarize Query:");
        Console.WriteLine(summarizeQuery.QueryText);

        // 2. Generate raw data query
        var rawDataQuery = generator.GenerateRawDataListQuery(source, viewByFields, filterFields);
        Console.WriteLine("\nRaw Data Query:");
        Console.WriteLine(rawDataQuery.QueryText);

        // 3. Get all available fields (excluding system fields)
        var excludedFields = new[] { "CreatedAt", "UpdatedAt", "IsDeleted" };
        var allFieldsQuery = generator.GenerateAllFieldsQuery(source, excludedFields);
        Console.WriteLine("\nAll Fields Query:");
        Console.WriteLine(allFieldsQuery.QueryText);

        // 4. Get all possible values for a specific field (with optional filters)
        var regionValuesQuery = generator.GenerateFieldValuesQuery(
            source,
            "Region",
            new List<IFilterField> 
            { 
                new FilterField 
                { 
                    Name = "AccountType", 
                    SpecifiedValues = new List<string> { "Partner" } 
                } 
            }
        );
        Console.WriteLine("\nField Values Query:");
        Console.WriteLine(regionValuesQuery.QueryText);
    }
}

### Query Generator Features

1. **GenerateSummarizeQuery**
   - Purpose: Generate aggregation query
   - Parameters:
     - `source`: Data source
     - `viewByFields`: Group by fields
     - `filterFields`: Filter conditions
     - `measurementFields`: Calculation metrics

2. **GenerateRawDataListQuery**
   - Purpose: Get raw data list
   - Parameters:
     - `source`: Data source
     - `viewByFields`: Fields to display (optional)
     - `filterFields`: Filter conditions (optional)

3. **GenerateAllFieldsQuery**
   - Purpose: Get all available fields and their data types
   - Parameters:
     - `source`: Data source
     - `excludedFields`: List of fields to exclude (optional)
   - Returns: Result set containing ColumnName and DataType

4. **GenerateFieldValuesQuery**
   - Purpose: Get all possible values for a specific field
   - Parameters:
     - `source`: Data source
     - `fieldName`: Name of the field to query
     - `filterFields`: Filter conditions (optional)
   - Returns: Result set containing unique values in Value column
