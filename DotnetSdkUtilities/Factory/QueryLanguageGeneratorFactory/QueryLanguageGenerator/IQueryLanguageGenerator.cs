using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common;
using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields;
using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator
{
    public interface IQueryLanguageGenerator
    {
        QueryDefinition GenerateSummarizeQuery(QuerySource source, IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields, IEnumerable<IMeasurementField> measurementFields);
        QueryDefinition GenerateRawDataListQuery(QuerySource source, IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields);
        QueryDefinition GenerateAllFieldsQuery(QuerySource source, IEnumerable<string> excludedFields = null);
        QueryDefinition GenerateLatestDataDateQuery();
        QueryDefinition GenerateFieldValuesQuery(QuerySource source, string fieldName, IEnumerable<IFilterField> filterFields = null);
    }
}
