using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.QueryParameters;
using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common
{
    public class QuerySource
    {
        public string Name { get; set; }
        public List<IQueryParameter> Parameters { get; set; } = new List<IQueryParameter>();
    }
}
