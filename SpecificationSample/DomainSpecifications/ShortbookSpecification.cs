using SpecificationSample.Model;
using SpecificationSample.Specification;
using System;
using System.Linq.Expressions;

namespace SpecificationSample.DomainSpecifications
{
    public class ShortbookSpecification : FilterSpecification<AudioBook>
    {
        public override Expression<Func<AudioBook, bool>> SpecificationExpression =>
            audioBook => audioBook.Duration < 500;
    }
}
