using System;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.QueryParameters
{
    public class QueryParameter : IQueryParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type DataType { get; set; }
    }
}
