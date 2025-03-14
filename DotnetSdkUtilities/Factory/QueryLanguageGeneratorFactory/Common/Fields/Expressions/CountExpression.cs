using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
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
