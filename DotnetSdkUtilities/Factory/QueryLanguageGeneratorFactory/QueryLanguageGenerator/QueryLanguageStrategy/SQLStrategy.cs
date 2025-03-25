using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy
{
    public class SQLStrategy : IQueryLanguageStrategy
    {
        public string ConvertSum(string field)
        {
            return $"SUM({field})";
        }

        public string ConvertCount()
        {
            return $"COUNT(*)";
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
            return $"COUNT(DISTINCT CASE WHEN {condition} THEN {field} ELSE NULL END)";
        }

        public string ConvertCountIf(string condition)
        {
            return $"COUNT(CASE WHEN {condition} THEN 1 ELSE NULL END)";
        }

        public string ConvertComposite(string @operator, IEnumerable<string> expressions)
        {
            return string.Join($" {@operator} ", expressions);
        }

        public string ConvertPredicate<T>(Expression<Func<T, bool>> expression)
        {
            if (expression.Body is BinaryExpression binaryExpression)
            {
                var left = binaryExpression.Left as MemberExpression;
                var right = binaryExpression.Right as ConstantExpression;

                if (left != null && right != null)
                {
                    var field = left.Member.Name;
                    var value = right.Value;

                    switch (binaryExpression.NodeType)
                    {
                        case ExpressionType.Equal:
                            return $"{field} = '{value}'";
                        case ExpressionType.NotEqual:
                            return $"{field} != '{value}'";
                        case ExpressionType.GreaterThan:
                            return $"{field} > '{value}'";
                        case ExpressionType.GreaterThanOrEqual:
                            return $"{field} >= '{value}'";
                        case ExpressionType.LessThan:
                            return $"{field} < '{value}'";
                        case ExpressionType.LessThanOrEqual:
                            return $"{field} <= '{value}'";
                    }
                }
            }
            else if (expression.Body is MemberExpression memberExpression)
            {
                // 處理布林屬性的情況，例如 x => x.IsOOXX
                return $"{memberExpression.Member.Name} = 1";
            }

            throw new NotSupportedException($"Expression type {expression.Body.NodeType} is not supported");
        }
    }
}
