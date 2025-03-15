using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCase.Factory.QueryLanguageGeneratorFactory
{
    [TestClass]
    public class QueryLanguageGeneratorFactoryTests
    {
        private IQueryLanguageGeneratorFactory _factory;
        private QuerySource _source;
        private List<IViewByField> _viewByFields;
        private List<IFilterField> _filterFields;
        private List<IMeasurementField> _measurementFields;

        [TestInitialize]
        public void Setup()
        {
            _factory = new DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGeneratorFactory();
            _source = new QuerySource { Name = "TestTable" };

            // Setup common test data
            _viewByFields = new List<IViewByField>
            {
                new ViewByField { Name = "Region" }
            };

            _filterFields = new List<IFilterField>
            {
                new FilterField 
                { 
                    Name = "Country", 
                    SpecifiedValues = new List<string> { "USA", "Canada" } 
                }
            };

            _measurementFields = new List<IMeasurementField>
            {
                new MeasurementField
                {
                    Name = "TotalRevenue",
                    Expression = new SumExpression("Revenue")
                }
            };
        }

        [TestMethod]
        public void CreateGenerator_WithValidType_ReturnsCorrectGenerator()
        {
            // Act
            var sqlGenerator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);
            var kqlGenerator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.KQL);

            // Assert
            Assert.IsInstanceOfType(sqlGenerator, typeof(SQLGenerator));
            Assert.IsInstanceOfType(kqlGenerator, typeof(KQLGenerator));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CreateGenerator_WithInvalidType_ThrowsException()
        {
            // Act
            _factory.CreateGenerator("InvalidType");
        }

        [TestMethod]
        public void RegisterGenerator_NewType_CanBeCreated()
        {
            // Arrange
            var customType = "CustomSQL";
            _factory.RegisterGenerator(customType, () => new SQLGenerator());

            // Act
            var generator = _factory.CreateGenerator(customType);

            // Assert
            Assert.IsNotNull(generator);
            Assert.IsInstanceOfType(generator, typeof(SQLGenerator));
        }

        [TestMethod]
        public void GenerateSummarizeQuery_SQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);

            // Act
            var result = generator.GenerateSummarizeQuery(_source, _viewByFields, _filterFields, _measurementFields);

            // Assert
            var expectedQuery = "SELECT Region, SUM(Revenue) AS TotalRevenue FROM TestTable " +
                              "WHERE Country IN ('USA', 'Canada') GROUP BY Region";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateSummarizeQuery_KQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.KQL);

            // Act
            var result = generator.GenerateSummarizeQuery(_source, _viewByFields, _filterFields, _measurementFields);

            // Assert
            var expectedQuery = "TestTable | where Country in ('USA', 'Canada') | " +
                              "summarize sum(Revenue) as TotalRevenue by Region";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateRawDataListQuery_SQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);

            // Act
            var result = generator.GenerateRawDataListQuery(_source, _viewByFields, _filterFields);

            // Assert
            var expectedQuery = "SELECT Region FROM TestTable WHERE Country IN ('USA', 'Canada')";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateRawDataListQuery_KQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.KQL);

            // Act
            var result = generator.GenerateRawDataListQuery(_source, _viewByFields, _filterFields);

            // Assert
            var expectedQuery = "TestTable | where Country in ('USA', 'Canada') | project Region";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateAllFieldsQuery_SQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);
            var excludedFields = new[] { "CreatedAt", "UpdatedAt" };

            // Act
            var result = generator.GenerateAllFieldsQuery(_source, excludedFields);

            // Assert
            var expectedQuery = "SELECT COLUMN_NAME as ColumnName, DATA_TYPE as DataType " +
                              "FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestTable' " +
                              "AND COLUMN_NAME NOT IN ('CreatedAt', 'UpdatedAt') ORDER BY COLUMN_NAME";
            Assert.AreEqual(expectedQuery.Replace(" ", ""), result.QueryText.Replace(" ", ""));
        }

        [TestMethod]
        public void GenerateAllFieldsQuery_KQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.KQL);
            var excludedFields = new[] { "CreatedAt", "UpdatedAt" };

            // Act
            var result = generator.GenerateAllFieldsQuery(_source, excludedFields);

            // Assert
            var expectedQuery = "TestTable | getschema | where ColumnName !in ('CreatedAt', 'UpdatedAt') " +
                              "| project ColumnName, DataType";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateFieldValuesQuery_SQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);

            // Act
            var result = generator.GenerateFieldValuesQuery(_source, "Region", _filterFields);

            // Assert
            var expectedQuery = "SELECT DISTINCT Region as Value FROM TestTable " +
                              "WHERE Country IN ('USA', 'Canada') ORDER BY Value";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateFieldValuesQuery_KQL_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.KQL);

            // Act
            var result = generator.GenerateFieldValuesQuery(_source, "Region", _filterFields);

            // Assert
            var expectedQuery = "TestTable | where Country in ('USA', 'Canada') | " +
                              "summarize Values = make_set(Region) | mv-expand Values | " +
                              "project Value = Values | sort by Value asc";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateFieldValuesQuery_WithNoFilters_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);

            // Act
            var result = generator.GenerateFieldValuesQuery(_source, "Region", null);

            // Assert
            var expectedQuery = "SELECT DISTINCT Region as Value FROM TestTable ORDER BY Value";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }

        [TestMethod]
        public void GenerateRawDataListQuery_WithNoViewByFields_GeneratesCorrectQuery()
        {
            // Arrange
            var generator = _factory.CreateGenerator(DefaultSupportQueryLanguageTypes.SQL);

            // Act
            var result = generator.GenerateRawDataListQuery(_source, new List<IViewByField>(), _filterFields);

            // Assert
            var expectedQuery = "SELECT * FROM TestTable WHERE Country IN ('USA', 'Canada')";
            Assert.AreEqual(expectedQuery, result.QueryText.Trim());
        }
    }
} 