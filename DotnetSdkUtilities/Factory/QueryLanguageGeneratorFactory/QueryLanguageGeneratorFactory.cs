using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator;
using System;
using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory
{
    public class QueryLanguageGeneratorFactory : IQueryLanguageGeneratorFactory
    {
        private readonly Dictionary<string, Func<IQueryLanguageGenerator>> _registry;

        public QueryLanguageGeneratorFactory()
        {
            _registry = new Dictionary<string, Func<IQueryLanguageGenerator>>();
            RegisterGenerator(DefaultSupportQueryLanguageTypes.KQL, () => new KQLGenerator());
            RegisterGenerator(DefaultSupportQueryLanguageTypes.SQL, () => new SQLGenerator());
        }

        public void RegisterGenerator(string languageType, Func<IQueryLanguageGenerator> generatorFactory)
        {
            _registry[languageType] = generatorFactory;
        }

        public IQueryLanguageGenerator CreateGenerator(string languageType)
        {
            if (_registry.TryGetValue(languageType, out var generatorFactory))
            {
                return generatorFactory();
            }

            throw new NotSupportedException($"Language {languageType} is not supported");
        }
    }
}
