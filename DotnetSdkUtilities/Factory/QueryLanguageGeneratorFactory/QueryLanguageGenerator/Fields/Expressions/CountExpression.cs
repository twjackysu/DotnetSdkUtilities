using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    public class CountExpression : IMeasurementExpression
    {
        public string Field { get; set; }

        public CountExpression(string field)
        {
            Field = field;
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertCount(Field);
        }
    }
}
