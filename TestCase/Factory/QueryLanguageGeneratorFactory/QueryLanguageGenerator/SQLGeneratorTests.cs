using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCase.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator
{
    [TestClass]
  public class SQLGeneratorTests
  {
    private SQLGenerator _generator;

    [TestInitialize]
    public void Setup()
    {
      _generator = new SQLGenerator();
    }

    [TestMethod]
    public void GenerateSummarizeQuery_WithValidInput_ReturnsCorrectQuery()
    {
      // Arrange
      var viewByFields = new List<IViewByField>
            {
                new ViewByField { Name = "Region" }
            };

      var filterFields = new List<IFilterField>
            {
                new FilterField { Name = "Region", SpecifiedValues = new List<string> { "Europe", "Americas" } }
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

      // Act
      var result = _generator.GenerateSummarizeQuery(viewByFields, filterFields, measurementFields);

      // Assert
      Assert.IsNotNull(result);
      Assert.IsTrue(result.QueryText.Contains("SELECT Region, SUM(ARR) / COUNT(DISTINCT Account_ID) AS AverageARR"));
      Assert.IsTrue(result.QueryText.Contains("WHERE Region IN ('Europe', 'Americas')"));
      Assert.IsTrue(result.QueryText.Contains("GROUP BY Region"));
    }
  }
}