concept
```csharp
var factory = new QueryLanguageGeneratorFactory();
var generator = factory.CreateGenerator(QueryLanguageType.SQL);

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

var queryDefinition = generator.GenerateSummarizeQuery(viewByFields, filterFields, measurementFields);
Console.WriteLine(queryDefinition.QueryText);
```