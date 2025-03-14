using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy
{
    public class KQLStrategy : IQueryLanguageStrategy
    {
        public string ConvertSum(string field)
        {
            return $"sum({field})";
        }

        public string ConvertCount(string field)
        {
            return $"count({field})";
        }

        public string ConvertAvg(string field)
        {
            return $"avg({field})";
        }

        public string ConvertMin(string field)
        {
            return $"min({field})";
        }

        public string ConvertMax(string field)
        {
            return $"max({field})";
        }

        public string ConvertCountDistinct(string field)
        {
            return $"dcount({field})";
        }

        public string ConvertDistinctCountIf(string field, string condition)
        {
            return $"dcountif({field}, {condition})";
        }

        public string ConvertCountIf(string field, string condition)
        {
            return $"countif({field}, {condition})";
        }

        public string ConvertComposite(string @operator, IEnumerable<string> expressions)
        {
            return string.Join($" {@operator} ", expressions);
        }
    }
}
