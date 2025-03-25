using System;
using System.Linq.Expressions;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields.Expressions.Predicates
{
    public class SimplePredicate<T> : IPredicate
    {
        public Expression<Func<T, bool>> Expression { get; }

        public SimplePredicate(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public string ToQueryString(IQueryLanguageStrategy strategy)
        {
            return strategy.ConvertPredicate(Expression);
        }
    }
}