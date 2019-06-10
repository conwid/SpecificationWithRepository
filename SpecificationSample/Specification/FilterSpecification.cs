using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationSample.Specification
{
    public static class FilterConverter
    {
        private class ConvertedSpecification<TType> : FilterSpecification<TType>
        {
            private readonly Expression<Func<TType, bool>> specificationExpression;
            public ConvertedSpecification(Expression<Func<TType, bool>> specificationExpression)
            {
                this.specificationExpression = specificationExpression;
            }

            public override Expression<Func<TType, bool>> SpecificationExpression => specificationExpression;
        }

        public static FilterSpecification<TDerived> ConvertSpecification<TBase, TDerived>(FilterSpecification<TBase> original) where TDerived : TBase
        {
            var expr1 = original.SpecificationExpression;
            var arg = Expression.Parameter(typeof(TDerived));
            var newBody = new ReplaceParameterVisitor { { expr1.Parameters.Single(), arg } }.Visit(expr1.Body);
            return new ConvertedSpecification<TDerived>(Expression.Lambda<Func<TDerived, bool>>(newBody, arg));
        }
    }
    public abstract class FilterSpecification<T>
    {
        private class ConstructedSpecification<TType> : FilterSpecification<TType>
        {
            private readonly Expression<Func<TType, bool>> specificationExpression;
            public ConstructedSpecification(Expression<Func<TType, bool>> specificationExpression)
            {
                this.specificationExpression = specificationExpression;
            }

            public override Expression<Func<TType, bool>> SpecificationExpression => specificationExpression;
        }

        public abstract Expression<Func<T, bool>> SpecificationExpression { get; }
        public static implicit operator Expression<Func<T, bool>>(FilterSpecification<T> spec) => spec.SpecificationExpression;
        public static FilterSpecification<T> operator &(FilterSpecification<T> left, FilterSpecification<T> right) => CombineSpecification(left, right, Expression.AndAlso);
        public static FilterSpecification<T> operator |(FilterSpecification<T> left, FilterSpecification<T> right) => CombineSpecification(left, right, Expression.OrElse);

        private static FilterSpecification<T> CombineSpecification(FilterSpecification<T> left, FilterSpecification<T> right, Func<Expression, Expression, BinaryExpression> combiner)
        {
            var expr1 = left.SpecificationExpression;
            var expr2 = right.SpecificationExpression;
            var arg = Expression.Parameter(typeof(T));
            var combined = combiner.Invoke(
                new ReplaceParameterVisitor { { expr1.Parameters.Single(), arg } }.Visit(expr1.Body),
                new ReplaceParameterVisitor { { expr2.Parameters.Single(), arg } }.Visit(expr2.Body));
            return new ConstructedSpecification<T>(Expression.Lambda<Func<T, bool>>(combined, arg));
        }

        public FilterSpecification<T> And(FilterSpecification<T> other)
        {
            return this & other;
        }

        public FilterSpecification<T> Or(FilterSpecification<T> other)
        {
            return this | other;
        }

        public static FilterSpecification<T> operator !(FilterSpecification<T> original)
        {
            var expr = original.SpecificationExpression;
            return new ConstructedSpecification<T>(Expression.Lambda<Func<T, bool>>(Expression.Negate(expr.Body), expr.Parameters));
        }

    }
}
