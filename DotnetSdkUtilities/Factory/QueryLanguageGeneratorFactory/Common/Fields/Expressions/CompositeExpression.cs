using DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageStrategy;
using System.Collections.Generic;
using System.Linq;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.Common.Fields.Expressions
{
    public class CompositeExpression : IMeasurementExpression
    {
        public List<IMeasurementExpression> Expressions { get; set; }
        public string Operator { get; set; }

        public CompositeExpression(string @operator, params IMeasurementExpression[] expressions)
        {
            Operator = @operator;
            Expressions = expressions.ToList();
        }

        public string ToQuery(IQueryLanguageStrategy strategy)
        {
            var expressionQueries = Expressions.Select(e => e.ToQuery(strategy));
            return strategy.ConvertComposite(Operator, expressionQueries);
        }
    }
}
