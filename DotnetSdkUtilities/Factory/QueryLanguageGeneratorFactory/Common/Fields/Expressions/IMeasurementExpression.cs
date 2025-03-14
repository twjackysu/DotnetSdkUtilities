using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
{
    public interface IMeasurementExpression
    {
        string ToQuery(IQueryLanguageStrategy strategy);
    }
}
