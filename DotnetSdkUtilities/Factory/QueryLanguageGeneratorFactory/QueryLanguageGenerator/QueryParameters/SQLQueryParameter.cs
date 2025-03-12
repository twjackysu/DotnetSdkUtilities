using System;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryParameters
{
    public class SQLQueryParameter : IQueryParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type DataType { get; set; }
    }
}
