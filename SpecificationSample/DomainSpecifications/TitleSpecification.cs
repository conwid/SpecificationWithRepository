using SpecificationSample.Model;
using SpecificationSample.Specification;
using System;
using System.Linq.Expressions;

namespace SpecificationSample.DomainSpecifications
{
    public class TitleSpecification : FilterSpecification<Book>
    {
        private readonly string title;
        public TitleSpecification(string title)
        {
            this.title = title;
        }
        public override Expression<Func<Book, bool>> SpecificationExpression =>
            book=> book.Title == title;
    }
}
