using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
{
    public class SumExpression : IMeasurementExpression
    {
        public string Field { get; set; }

        public SumExpression(string field)
        {
            Field = field;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertSum(Field);
        }
    }
}
