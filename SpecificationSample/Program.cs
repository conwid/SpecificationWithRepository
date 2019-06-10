using SpecificationSample.DomainSpecifications;
using SpecificationSample.Model;
using SpecificationSample.Specification;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var spec1 = new ShortbookSpecification();
            var spec2 = new AuthorSpecification("Author1");
            using (var ctx = new BookContext())
            {
                var repository = new AudioBookRepository(ctx);
                var spec2Converted = FilterConverter.ConvertSpecification<Book, AudioBook>(spec2);
                var results = repository.List(spec2Converted);
            }
        }
    }
}
