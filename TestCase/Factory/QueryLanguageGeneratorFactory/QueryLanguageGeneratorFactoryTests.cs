using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCase.Factory.QueryLanguageGeneratorFactory
{
  [TestClass]
  public class QueryLanguageGeneratorFactoryTests
  {
    private IQueryLanguageGeneratorFactory _factory;

    [TestInitialize]
    public void Setup()
    {
      _factory = new DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGeneratorFactory();
    }

    [TestMethod]
    public void CreateGenerator_WithSQLType_ReturnsSQLGenerator()
    {
      // Arrange
      var languageType = QueryLanguageType.SQL;

      // Act
      var generator = _factory.CreateGenerator(languageType);

      // Assert
      Assert.IsInstanceOfType(generator, typeof(SQLGenerator));
    }

    [TestMethod]
    public void CreateGenerator_WithKQLType_ReturnsKQLGenerator()
    {
      // Arrange
      var languageType = QueryLanguageType.KQL;

      // Act
      var generator = _factory.CreateGenerator(languageType);

      // Assert
      Assert.IsInstanceOfType(generator, typeof(KQLGenerator));
    }

    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void CreateGenerator_WithUnsupportedType_ThrowsNotSupportedException()
    {
      // Arrange
      var languageType = (QueryLanguageType)999;

      // Act & Assert
      _factory.CreateGenerator(languageType);
    }
  }
}