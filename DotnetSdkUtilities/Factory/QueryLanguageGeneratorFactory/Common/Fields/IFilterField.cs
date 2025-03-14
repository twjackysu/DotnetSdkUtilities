using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields
{
    public interface IFilterField : IField
    {
        IEnumerable<string> SpecifiedValues { get; set; }
    }
}
