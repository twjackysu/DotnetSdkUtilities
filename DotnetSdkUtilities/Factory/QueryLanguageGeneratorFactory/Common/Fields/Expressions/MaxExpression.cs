using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
{
    public class MaxExpression : IMeasurementExpression
    {
        public string Field { get; set; }

        public MaxExpression(string field)
        {
            Field = field;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertMax(Field);
        }
    }
}
