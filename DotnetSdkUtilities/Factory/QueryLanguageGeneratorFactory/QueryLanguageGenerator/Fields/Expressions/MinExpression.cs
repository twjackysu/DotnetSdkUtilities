using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
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
