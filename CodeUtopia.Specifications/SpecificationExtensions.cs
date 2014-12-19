﻿namespace CodeUtopia.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> other)
        {
            return new NotSpecification<T>(other);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }
    }
}