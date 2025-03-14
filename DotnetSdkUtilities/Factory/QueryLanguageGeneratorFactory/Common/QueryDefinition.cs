using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.QueryParameters;
using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common
{
    public class QueryDefinition
    {
        public string QueryText { get; set; }
        public List<IQueryParameter> Parameters { get; set; }

        public QueryDefinition(string queryText, List<IQueryParameter> parameters)
        {
            QueryText = queryText;
            Parameters = parameters;
        }
    }
}
