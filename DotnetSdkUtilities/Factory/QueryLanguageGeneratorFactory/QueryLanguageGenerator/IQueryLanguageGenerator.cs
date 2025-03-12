using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.Fields;
using System.Collections.Generic;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator
{
    public interface IQueryLanguageGenerator
    {
        QueryDefinition GenerateSummarizeQuery(IEnumerable<IViewByField> viewByFields, IEnumerable<IFilterField> filterFields, IEnumerable<IMeasurementField> measurementFields);
        QueryDefinition GenerateRawDataListQuery();
        QueryDefinition GenerateAllFieldsQuery();
        QueryDefinition GenerateLatestDataDateQuery();
        QueryDefinition GenerateFieldValuesQuery();
    }
}
