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

        public QueryDefinition GenerateSummarizeQuery()
        {
            return new QueryDefinition("KQL Summarize Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateRawDataListQuery()
        {
            return new QueryDefinition("KQL Raw Data List Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateAllFieldsQuery()
        {
            return new QueryDefinition("KQL All Fields Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateLatestDataDateQuery()
        {
            return new QueryDefinition("KQL Latest Data Date Query", new List<IQueryParameter>());
        }

        public QueryDefinition GenerateFieldValuesQuery()
        {
            return new QueryDefinition("KQL Field Values Query", new List<IQueryParameter>());
        }
    }
}
