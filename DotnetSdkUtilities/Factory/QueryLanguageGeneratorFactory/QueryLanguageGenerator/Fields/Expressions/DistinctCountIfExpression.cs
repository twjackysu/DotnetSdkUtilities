using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    public class DistinctCountIfExpression : IMeasurementExpression
    {
        public string Field { get; }
        public IPredicate Predicate { get; }

        public DistinctCountIfExpression(string field, IPredicate predicate)
        {
            Field = field;
            Predicate = predicate;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertDistinctCountIf(Field, Predicate.ToQueryString(strategy));
        }
    }
}
