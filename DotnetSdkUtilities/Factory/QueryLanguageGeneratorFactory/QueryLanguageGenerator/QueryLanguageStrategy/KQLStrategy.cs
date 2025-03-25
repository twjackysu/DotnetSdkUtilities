using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotnetSdkUtilities.Factory.QueryLanguageGeneratorFactory.QueryLanguageGenerator.QueryLanguageStrategy
{
    public class KQLStrategy : IQueryLanguageStrategy
    {
        public string ConvertSum(string field)
        {
            return $"sum({field})";
        }

        public string ConvertCount()
        {
            return $"count()";
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

        public string ConvertCountIf(string condition)
        {
            return $"countif({condition})";
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
                            return $"{field} == '{value}'";
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
                return $"{memberExpression.Member.Name} == true";
            }

            throw new NotSupportedException($"Expression type {expression.Body.NodeType} is not supported");
        }
    }
}
