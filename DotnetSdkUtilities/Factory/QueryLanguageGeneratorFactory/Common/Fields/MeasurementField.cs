using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields
{
    public class MeasurementField : IMeasurementField
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IMeasurementExpression Expression { get; set; }
    }
}
