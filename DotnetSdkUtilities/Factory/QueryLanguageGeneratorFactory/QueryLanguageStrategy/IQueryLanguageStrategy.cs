using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy
{
    public interface IQueryLanguageStrategy
    {
        string ConvertSum(string field);
        string ConvertCount(string field);
        string ConvertAvg(string field);
        string ConvertMin(string field);
        string ConvertMax(string field);
        string ConvertCountDistinct(string field);
        string ConvertDistinctCountIf(string field, string condition);
        string ConvertCountIf(string field, string condition);
        string ConvertComposite(string @operator, IEnumerable<string> expressions);
    }
}
