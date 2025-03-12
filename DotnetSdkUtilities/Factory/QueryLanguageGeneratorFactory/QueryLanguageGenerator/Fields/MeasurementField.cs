using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields
{
    public class MeasurementField : IMeasurementField
    {
        public string Name { get; set; }
        public IMeasurementExpression Expression { get; set; }
    }
}
