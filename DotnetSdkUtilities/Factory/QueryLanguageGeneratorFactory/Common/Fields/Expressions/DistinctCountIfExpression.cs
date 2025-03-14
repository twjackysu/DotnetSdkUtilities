using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
{
    public class DistinctCountIfExpression : IMeasurementExpression
    {
        public string Field { get; set; }
        public string Condition { get; set; }

        public DistinctCountIfExpression(string field, string condition)
        {
            Field = field;
            Condition = condition;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertDistinctCountIf(Field, Condition);
        }
    }
}
