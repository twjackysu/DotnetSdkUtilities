using System;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryParameters
{
    public interface IQueryParameter
    {
        string Name { get; }
        object Value { get; }
        Type DataType { get; }
    }
}
