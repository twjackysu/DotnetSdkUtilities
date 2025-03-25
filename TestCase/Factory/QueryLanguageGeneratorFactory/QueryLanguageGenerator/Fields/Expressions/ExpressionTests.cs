
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCase.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace TestCase.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    [TestClass]
    public class ExpressionTests
    {
        private SQLStrategy _sqlStrategy;
        private KQLStrategy _kqlStrategy;

        [TestInitialize]
        public void Setup()
        {
            _sqlStrategy = new SQLStrategy();
            _kqlStrategy = new KQLStrategy();
        }

        [TestMethod]
        public void SumExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new SumExpression("Amount");

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("SUM(Amount)", sqlResult);
            Assert.AreEqual("sum(Amount)", kqlResult);
        }

        [TestMethod]
        public void CountExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new CountExpression();

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("COUNT(*)", sqlResult);
            Assert.AreEqual("count()", kqlResult);
        }

        [TestMethod]
        public void CountDistinctExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new CountDistinctExpression("UserID");

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("COUNT(DISTINCT UserID)", sqlResult);
            Assert.AreEqual("dcount(UserID)", kqlResult);
        }

        [TestMethod]
        public void AvgExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new AvgExpression("Price");

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("AVG(Price)", sqlResult);
            Assert.AreEqual("avg(Price)", kqlResult);
        }

        [TestMethod]
        public void MinExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new MinExpression("Value");

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("MIN(Value)", sqlResult);
            Assert.AreEqual("min(Value)", kqlResult);
        }

        [TestMethod]
        public void MaxExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new MaxExpression("Value");

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("MAX(Value)", sqlResult);
            Assert.AreEqual("max(Value)", kqlResult);
        }

        [TestMethod]
        public void CountIfExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var predicate = new SimplePredicate<TestDTO>(x => x.Status == "Active");
            var expression = new CountIfExpression(predicate);

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("COUNT(CASE WHEN Status = 'Active' THEN 1 ELSE NULL END)", sqlResult);
            Assert.AreEqual("countif(Status == 'Active')", kqlResult);
        }

        [TestMethod]
        public void DistinctCountIfExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var predicate = new SimplePredicate<TestDTO>(x => x.Status == "Active");
            var expression = new DistinctCountIfExpression("UserID", predicate);

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("COUNT(DISTINCT CASE WHEN Status = 'Active' THEN UserID ELSE NULL END)", sqlResult);
            Assert.AreEqual("dcountif(UserID, Status == 'Active')", kqlResult);
        }

        [TestMethod]
        public void CompositeExpression_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new CompositeExpression(
                "/",
                new SumExpression("Amount"),
                new CountDistinctExpression("UserID")
            );

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("SUM(Amount) / COUNT(DISTINCT UserID)", sqlResult);
            Assert.AreEqual("sum(Amount) / dcount(UserID)", kqlResult);
        }

        [TestMethod]
        public void CompositeExpression_WithMultipleExpressions_GeneratesCorrectQueries()
        {
            // Arrange
            var expression = new CompositeExpression(
                "*",
                new SumExpression("Amount"),
                new CountDistinctExpression("UserID"),
                new AvgExpression("Price")
            );

            // Act
            var sqlResult = expression.ToQuery(_sqlStrategy);
            var kqlResult = expression.ToQuery(_kqlStrategy);

            // Assert
            Assert.AreEqual("SUM(Amount) * COUNT(DISTINCT UserID) * AVG(Price)", sqlResult);
            Assert.AreEqual("sum(Amount) * dcount(UserID) * avg(Price)", kqlResult);
        }
    }
}