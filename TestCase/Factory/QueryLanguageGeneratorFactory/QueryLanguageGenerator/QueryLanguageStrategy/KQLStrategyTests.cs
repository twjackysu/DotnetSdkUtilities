
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCase.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy
{
    [TestClass]
    public class KQLStrategyTests
    {
        private KQLStrategy _strategy;

        [TestInitialize]
        public void Setup()
        {
            _strategy = new KQLStrategy();
        }

        [TestMethod]
        public void ConvertSum_ReturnsCorrectKQL()
        {
            // Act
            var result = _strategy.ConvertSum("Amount");

            // Assert
            Assert.AreEqual("sum(Amount)", result);
        }

        [TestMethod]
        public void ConvertCount_ReturnsCorrectKQL()
        {
            // Act
            var result = _strategy.ConvertCount();

            // Assert
            Assert.AreEqual("count()", result);
        }

        [TestMethod]
        public void ConvertAvg_ReturnsCorrectKQL()
        {
            // Act
            var result = _strategy.ConvertAvg("Price");

            // Assert
            Assert.AreEqual("avg(Price)", result);
        }

        [TestMethod]
        public void ConvertMin_ReturnsCorrectKQL()
        {
            // Act
            var result = _strategy.ConvertMin("Value");

            // Assert
            Assert.AreEqual("min(Value)", result);
        }

        [TestMethod]
        public void ConvertMax_ReturnsCorrectKQL()
        {
            // Act
            var result = _strategy.ConvertMax("Value");

            // Assert
            Assert.AreEqual("max(Value)", result);
        }

        [TestMethod]
        public void ConvertCountDistinct_ReturnsCorrectKQL()
        {
            // Act
            var result = _strategy.ConvertCountDistinct("UserID");

            // Assert
            Assert.AreEqual("dcount(UserID)", result);
        }

        [TestMethod]
        public void ConvertDistinctCountIf_ReturnsCorrectKQL()
        {
         
            var predicate = new SimplePredicate<TestDTO>(x => x.Status == "Active").ToQueryString(_strategy);

            // Act
            var result = _strategy.ConvertDistinctCountIf("UserID", predicate);

            // Assert
            Assert.AreEqual("dcountif(UserID, Status == 'Active')", result);
        }

        [TestMethod]
        public void ConvertCountIf_ReturnsCorrectKQL()
        {
            var predicate = new SimplePredicate<TestDTO>(x => x.Status == "Active").ToQueryString(_strategy);

            // Act
            var result = _strategy.ConvertCountIf(predicate);

            // Assert
            Assert.AreEqual("countif(ID, Status == 'Active')", result);
        }

        [TestMethod]
        public void ConvertComposite_ReturnsCorrectKQL()
        {
            var expressions = new string[] { _strategy.ConvertSum("Amount"), _strategy.ConvertCountDistinct("UserID") };

            // Act
            var result = _strategy.ConvertComposite("/", expressions);

            // Assert
            Assert.AreEqual("sum(Amount) / dcount(UserID)", result);
        }
    }
}