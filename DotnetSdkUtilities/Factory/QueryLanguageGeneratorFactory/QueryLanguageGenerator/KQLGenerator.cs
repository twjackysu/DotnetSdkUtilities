using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.QueryParameters;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;
using System.Collections.Generic;
using System.Linq;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator
{
    public class KQLGenerator : IQueryLanguageGenerator
    {
        private readonly IQueryLanguageStrategy _strategy = new KQLStrategy();

        public QueryDefinition GenerateSummarizeQuery(QuerySource source, IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields, IEnumerable<IMeasurementField> measurementFields)
        {
            var viewByClause = string.Join(", ", viewByFields.Select(f => f.Name));
            var filterClause = string.Join(" and ", filterFields.Select(f => $"{f.Name} in ({string.Join(", ", f.SpecifiedValues.Select(v => $"'{v}'"))})"));
            var measurementClause = string.Join(", ", measurementFields.Select(f => $"{f.Expression.ToQuery(_strategy)} as {f.Name}"));

            var queryText = $"{source.Name} | where {filterClause} | summarize {measurementClause} by {viewByClause}";
            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateRawDataListQuery(QuerySource source, IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields)
        {
            var selectClause = string.Join(", ", viewByFields.Select(f => f.Name));
            var filterClause = string.Join(" and ", filterFields.Select(f => $"{f.Name} in ({string.Join(", ", f.SpecifiedValues.Select(v => $"'{v}'"))})"));

            var queryText = $"{source.Name}";
            if (!string.IsNullOrEmpty(filterClause))
            {
                queryText += $" | where {filterClause}";
            }
            if (!string.IsNullOrEmpty(selectClause))
            {
                queryText += $" | project {selectClause}";
            }

            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateAllFieldsQuery(QuerySource source, IEnumerable<string> excludedFields = null)
        {
            var excludedFieldsSet = excludedFields?.ToHashSet() ?? new HashSet<string>();
            
            var queryText = $"{source.Name} | getschema";
            
            if (excludedFieldsSet.Any())
            {
                var excludedFieldsStr = string.Join(", ", excludedFieldsSet.Select(f => $"'{f}'"));
                queryText += $" | where ColumnName !in ({excludedFieldsStr})";
            }
            
            queryText += " | project ColumnName, DataType";

            return new QueryDefinition(queryText, source.Parameters);
        }

        public QueryDefinition GenerateLatestDataDateQuery()
        {
            return new QueryDefinition("KQL Latest Data Date Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateFieldValuesQuery(QuerySource source, string fieldName, IEnumerable<IFilterField> filterFields = null)
        {
            var queryText = $"{source.Name}";

            if (filterFields != null && filterFields.Any())
            {
                var filterClause = string.Join(" and ", filterFields.Select(f => $"{f.Name} in ({string.Join(", ", f.SpecifiedValues.Select(v => $"'{v}'"))})"));
                queryText += $" | where {filterClause}";
            }

            queryText += $" | summarize Values = make_set({fieldName})";
            
            queryText += " | mv-expand Values";
            
            queryText += " | project Value = Values | sort by Value asc";

            return new QueryDefinition(queryText, source.Parameters);
        }
    }
}
