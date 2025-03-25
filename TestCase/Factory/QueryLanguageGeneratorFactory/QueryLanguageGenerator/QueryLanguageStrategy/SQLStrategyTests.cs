using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCase.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy
{
    [TestClass]
    public class SQLStrategyTests
    {
        private SQLStrategy _strategy;

        [TestInitialize]
        public void Setup()
        {
            _strategy = new SQLStrategy();
        }

        [TestMethod]
        public void ConvertSum_ReturnsCorrectSQL()
        {
            // Act
            var result = _strategy.ConvertSum("Amount");

            // Assert
            Assert.AreEqual("SUM(Amount)", result);
        }

        [TestMethod]
        public void ConvertCount_ReturnsCorrectSQL()
        {
            // Act
            var result = _strategy.ConvertCount();

            // Assert
            Assert.AreEqual("COUNT(*)", result);
        }

        [TestMethod]
        public void ConvertAvg_ReturnsCorrectSQL()
        {
            // Act
            var result = _strategy.ConvertAvg("Price");

            // Assert
            Assert.AreEqual("AVG(Price)", result);
        }

        [TestMethod]
        public void ConvertMin_ReturnsCorrectSQL()
        {
            // Act
            var result = _strategy.ConvertMin("Value");

            // Assert
            Assert.AreEqual("MIN(Value)", result);
        }

        [TestMethod]
        public void ConvertMax_ReturnsCorrectSQL()
        {
            // Act
            var result = _strategy.ConvertMax("Value");

            // Assert
            Assert.AreEqual("MAX(Value)", result);
        }

        [TestMethod]
        public void ConvertCountDistinct_ReturnsCorrectSQL()
        {
            // Act
            var result = _strategy.ConvertCountDistinct("UserID");

            // Assert
            Assert.AreEqual("COUNT(DISTINCT UserID)", result);
        }

        [TestMethod]
        public void ConvertDistinctCountIf_ReturnsCorrectSQL()
        {
            var predicate = new SimplePredicate<TestDTO>(x => x.Status == "Active").ToQueryString(_strategy);

            // Act
            var result = _strategy.ConvertDistinctCountIf("UserID", predicate);

            // Assert
            Assert.AreEqual("COUNT(DISTINCT CASE WHEN Status = 'Active' THEN UserID ELSE NULL END)", result);
        }

        [TestMethod]
        public void ConvertCountIf_ReturnsCorrectSQL()
        {
            var predicate = new SimplePredicate<TestDTO>(x => x.Status == "Active").ToQueryString(_strategy);

            // Act
            var result = _strategy.ConvertCountIf(predicate);

            // Assert
            Assert.AreEqual("COUNT(CASE WHEN Status = 'Active' THEN 1 ELSE NULL END)", result);
        }

        [TestMethod]
        public void ConvertComposite_ReturnsCorrectSQL()
        {
            // Arrange
            var expressions = new string[] { _strategy.ConvertSum("Amount"), _strategy.ConvertCountDistinct("UserID") };

            // Act
            var result = _strategy.ConvertComposite("/", expressions);

            // Assert
            Assert.AreEqual("SUM(Amount) / COUNT(DISTINCT UserID)", result);
        }
    }
}