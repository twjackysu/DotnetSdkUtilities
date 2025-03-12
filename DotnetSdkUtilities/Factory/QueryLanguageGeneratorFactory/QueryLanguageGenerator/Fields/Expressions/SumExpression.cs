using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
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
