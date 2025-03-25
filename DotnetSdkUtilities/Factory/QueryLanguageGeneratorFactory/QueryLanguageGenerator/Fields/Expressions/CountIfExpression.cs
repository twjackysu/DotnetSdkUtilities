using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    public class CountIfExpression : IMeasurementExpression
    {
        public IPredicate Predicate { get; }

        public CountIfExpression(IPredicate predicate)
        {
            Predicate = predicate;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertCountIf(Predicate.ToQueryString(strategy));
        }
    }
}
