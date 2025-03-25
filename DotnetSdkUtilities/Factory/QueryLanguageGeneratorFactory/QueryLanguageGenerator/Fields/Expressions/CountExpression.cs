using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions
{
    public class CountExpression : IMeasurementExpression
    {
        public CountExpression()
        {
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertCount();
        }
    }
}
