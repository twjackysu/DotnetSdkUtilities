using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields
{
    public interface IMeasurementField : IField
    {
        IMeasurementExpression Expression { get; set; }
    }
}
