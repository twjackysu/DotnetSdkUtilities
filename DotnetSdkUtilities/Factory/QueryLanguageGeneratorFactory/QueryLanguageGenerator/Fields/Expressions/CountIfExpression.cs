using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    public class CountIfExpression : IMeasurementExpression
    {
        public string Field { get; set; }
        public string Condition { get; set; }

        public CountIfExpression(string field, string condition)
        {
            Field = field;
            Condition = condition;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertCountIf(Field, Condition);
        }
    }
}
