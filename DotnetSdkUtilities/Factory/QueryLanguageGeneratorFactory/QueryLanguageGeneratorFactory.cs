using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using System;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory
{
    public class QueryLanguageGeneratorFactory : IQueryLanguageGeneratorFactory
    {
        public IQueryLanguageGenerator CreateGenerator(QueryLanguageType languageType)
        {
            return languageType switch
            {
                QueryLanguageType.KQL => new KQLGenerator(),
                QueryLanguageType.SQL => new SQLGenerator(),
                _ => throw new NotSupportedException($"Language {languageType} is not supported")
            };
        }
    }
}
