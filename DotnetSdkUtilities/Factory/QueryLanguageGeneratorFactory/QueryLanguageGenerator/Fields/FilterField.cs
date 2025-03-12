using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields
{
    public class FilterField : IFilterField
    {
        public string Name { get; set; }
        public IEnumerable<string> SpecifiedValues { get; set; }
    }
}
