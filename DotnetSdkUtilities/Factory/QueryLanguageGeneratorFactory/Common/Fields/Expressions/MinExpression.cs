using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
{
    public class MinExpression : IMeasurementExpression
    {
        public string Field { get; set; }

        public MinExpression(string field)
        {
            Field = field;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertMin(Field);
        }
    }
}
