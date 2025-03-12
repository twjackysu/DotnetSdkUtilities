using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields
{
    public interface IMeasurementField : IField
    {
        IMeasurementExpression Expression { get; set; }
    }
}
