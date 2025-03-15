using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.QueryParameters;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;
using System.Collections.Generic;
using System.Linq;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator
{
    public class SQLGenerator : IQueryLanguageGenerator
    {
        private readonly IQueryLanguageStrategy _strategy = new SQLStrategy();

        public QueryDefinition GenerateSummarizeQuery(QuerySource source, IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields, IEnumerable<IMeasurementField> measurementFields)
        {
            var viewByClause = string.Join(", ", viewByFields.Select(f => f.Name));
            var filterClause = string.Join(" AND ", filterFields.Select(f => $"{f.Name} IN ({string.Join(", ", f.SpecifiedValues.Select(v => $"'{v}'"))})"));
            var measurementClause = string.Join(", ", measurementFields.Select(f => $"{f.Expression.ToQuery(_strategy)} AS {f.Name}"));

            var queryText = $"SELECT {viewByClause}, {measurementClause} FROM {source.Name} WHERE {filterClause} GROUP BY {viewByClause}";
            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateRawDataListQuery(QuerySource source, IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields)
        {
            var selectClause = viewByFields.Any() ? string.Join(", ", viewByFields.Select(f => f.Name)) : "*";
            var filterClause = string.Join(" AND ", filterFields.Select(f => $"{f.Name} IN ({string.Join(", ", f.SpecifiedValues.Select(v => $"'{v}'"))})"));

            var queryText = $"SELECT {selectClause} FROM {source.Name}";
            if (!string.IsNullOrEmpty(filterClause))
            {
                queryText += $" WHERE {filterClause}";
            }

            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateAllFieldsQuery(QuerySource source, IEnumerable<string> excludedFields = null)
        {
            var excludedFieldsSet = excludedFields?.ToHashSet() ?? new HashSet<string>();
            
            // 使用 INFORMATION_SCHEMA.COLUMNS 來獲取所有欄位資訊
            var queryText = $@"
                SELECT 
                    COLUMN_NAME as ColumnName, 
                    DATA_TYPE as DataType
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_NAME = '{source.Name}'";

            // 如果有需要排除的欄位，添加 WHERE 子句
            if (excludedFieldsSet.Any())
            {
                var excludedFieldsStr = string.Join(", ", excludedFieldsSet.Select(f => $"'{f}'"));
                queryText += $" AND COLUMN_NAME NOT IN ({excludedFieldsStr})";
            }

            // 按欄位名稱排序
            queryText += " ORDER BY COLUMN_NAME";

            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateLatestDataDateQuery()
        {
            return new QueryDefinition("SQL Latest Data Date Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateFieldValuesQuery(QuerySource source, string fieldName, IEnumerable<IFilterField> filterFields = null)
        {
            var queryText = $"SELECT DISTINCT {fieldName} as Value FROM {source.Name}";

            // 添加過濾條件（如果有的話）
            if (filterFields != null && filterFields.Any())
            {
                var filterClause = string.Join(" AND ", filterFields.Select(f => $"{f.Name} IN ({string.Join(", ", f.SpecifiedValues.Select(v => $"'{v}'"))})"));
                queryText += $" WHERE {filterClause}";
            }

            // 按值排序
            queryText += " ORDER BY Value";

            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateSummarizeQuery()
        {
            return new QueryDefinition("SQL Summarize Query", new List<IQueryParameter>());
        }
    }
}
