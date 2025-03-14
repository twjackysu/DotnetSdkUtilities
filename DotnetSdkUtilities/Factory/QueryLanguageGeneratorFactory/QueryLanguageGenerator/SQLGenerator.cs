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

        public QueryDefinition GenerateRawDataListQuery()
        {
            return new QueryDefinition("SQL Raw Data List Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateAllFieldsQuery()
        {
            return new QueryDefinition("SQL All Fields Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateLatestDataDateQuery()
        {
            return new QueryDefinition("SQL Latest Data Date Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateFieldValuesQuery()
        {
            return new QueryDefinition("SQL Field Values Query", new List<IQueryParameter>());
        }
    }
}
