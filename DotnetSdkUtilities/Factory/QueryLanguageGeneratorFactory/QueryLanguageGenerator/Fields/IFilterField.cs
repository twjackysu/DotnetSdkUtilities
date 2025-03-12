using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields
{
    public interface IFilterField : IField
    {
        IEnumerable<string> SpecifiedValues { get; set; }
    }
}
