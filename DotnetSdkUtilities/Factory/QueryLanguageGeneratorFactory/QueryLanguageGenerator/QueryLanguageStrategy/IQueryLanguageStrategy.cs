using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy
{
    public interface IQueryLanguageStrategy
    {
        string ConvertSum(string field);
        string ConvertCount();
        string ConvertAvg(string field);
        string ConvertMin(string field);
        string ConvertMax(string field);
        string ConvertCountDistinct(string field);
        string ConvertDistinctCountIf(string field, string condition);
        string ConvertCountIf(string condition);
        string ConvertComposite(string @operator, IEnumerable<string> expressions);
        string ConvertPredicate<T>(Expression<Func<T, bool>> expression);
    }
}
