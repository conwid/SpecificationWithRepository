using SpecificationSample.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationSample.Model
{
    public interface IRepository<T>
    {
        IReadOnlyList<T> List(FilterSpecification<T> filterSpecification);
    }
    public class AudioBookRepository : IRepository<AudioBook>
    {
        private readonly BookContext bookContext;
        public AudioBookRepository(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public IReadOnlyList<AudioBook> List(FilterSpecification<AudioBook> filterSpecification)
        {
            return this.bookContext.AudioBooks.Where(filterSpecification).ToList();
        }

        // Does not work
        //public IReadOnlyList<AudioBook> List<TBase>(FilterSpecification<TBase> filterSpecification) where AudioBook : TBase
        //{
        //    return this.bookContext.AudioBooks.Where(FilterConverter.ConvertSpecification<TBase, AudioBook>(filterSpecification)).ToList();
        //}
    }
}
