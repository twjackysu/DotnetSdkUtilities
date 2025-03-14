using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy
{
    public class SQLStrategy : IQueryLanguageStrategy
    {
        public string ConvertSum(string field)
        {
            return $"SUM({field})";
        }

        public string ConvertCount(string field)
        {
            return $"COUNT({field})";
        }

        public string ConvertAvg(string field)
        {
            return $"AVG({field})";
        }

        public string ConvertMin(string field)
        {
            return $"MIN({field})";
        }

        public string ConvertMax(string field)
        {
            return $"MAX({field})";
        }

        public string ConvertCountDistinct(string field)
        {
            return $"COUNT(DISTINCT {field})";
        }

        public string ConvertDistinctCountIf(string field, string condition)
        {
            return $"COUNT(DISTINCT CASE WHEN {condition} THEN {field} END)";
        }

        public string ConvertCountIf(string field, string condition)
        {
            return $"COUNT(CASE WHEN {condition} THEN {field} END)";
        }

        public string ConvertComposite(string @operator, IEnumerable<string> expressions)
        {
            return string.Join($" {@operator} ", expressions);
        }
    }
}
