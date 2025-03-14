using System;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.QueryParameters
{
    public interface IQueryParameter
    {
        string Name { get; }
        object Value { get; }
        Type DataType { get; }
    }
}
