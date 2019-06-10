using SpecificationSample.Model;
using SpecificationSample.Specification;
using System;
using System.Linq.Expressions;

namespace SpecificationSample.DomainSpecifications
{
    public class AuthorSpecification : FilterSpecification<Book>
    {
        private readonly string author;
        public AuthorSpecification(string author)
        {
            this.author = author;
        }
        public override Expression<Func<Book, bool>> SpecificationExpression =>
            book => book.Author == author;
    }
}
