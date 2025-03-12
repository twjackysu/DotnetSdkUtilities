using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    public class CountDistinctExpression : IMeasurementExpression
    {
        public string Field { get; set; }

        public CountDistinctExpression(string field)
        {
            Field = field;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertCountDistinct(Field);
        }
    }
}
