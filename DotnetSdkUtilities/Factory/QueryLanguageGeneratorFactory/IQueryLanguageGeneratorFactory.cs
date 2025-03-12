using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory
{
    public interface IQueryLanguageGeneratorFactory
    {
        IQueryLanguageGenerator CreateGenerator(QueryLanguageType languageType);
    }
}
