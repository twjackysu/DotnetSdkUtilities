using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using System;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory
{
    public interface IQueryLanguageGeneratorFactory
    {
        void RegisterGenerator(string languageType, Func<IQueryLanguageGenerator> generatorFactory);
        IQueryLanguageGenerator CreateGenerator(string languageType);
    }
}
