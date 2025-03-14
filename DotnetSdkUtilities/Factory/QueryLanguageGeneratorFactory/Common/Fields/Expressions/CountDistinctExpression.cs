using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
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
