using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields
{
    public class FilterField : IFilterField
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> SpecifiedValues { get; set; }
    }
}
