using System;
using System.Linq.Expressions;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates
{
    public interface IPredicate
    {
        string ToQueryString(IQueryLanguageStrategy strategy);
    }
}