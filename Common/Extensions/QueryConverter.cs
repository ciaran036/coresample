using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Extensions
{
    public static class QueryConverter
    {
        public static IQueryable<T> Convert<T>(this IQueryable<T> source)
        {
            var expression = new ExpressionConverter().Visit(source.Expression);
            if (expression == source.Expression) return source;
            return source.Provider.CreateQuery<T>(expression);
        }

        class ExpressionConverter : ExpressionVisitor
        {
            static readonly MethodInfo EnumDescriptionMethod = Expression.Call(
                    typeof(EnumExtensions), nameof(EnumExtensions.Description), new[] { typeof(ExpressionType) },
                    Expression.Constant(default(ExpressionType)))
                .Method.GetGenericMethodDefinition();

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == EnumDescriptionMethod)
                    return TranslateEnumDescription(Visit(node.Arguments[0]));
                return base.VisitMethodCall(node);
            }

            static Expression TranslateEnumDescription(Expression arg)
            {
                var names = Enum.GetNames(arg.Type);
                var values = Enum.GetValues(arg.Type);
                Expression result = Expression.Constant("");
                for (int i = names.Length - 1; i >= 0; i--)
                {
                    var value = values.GetValue(i);
                    var description = arg.Type.GetField(names[i], BindingFlags.Public | BindingFlags.Static).Description();
                    // arg == value ? description : ...
                    result = Expression.Condition(
                        Expression.Equal(arg, Expression.Constant(value)),
                        Expression.Constant(description),
                        result);
                }
                return result;
            }
        }
    }
}