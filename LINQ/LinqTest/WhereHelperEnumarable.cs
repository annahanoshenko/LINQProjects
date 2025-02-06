using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ.LinqTest
{
    internal class WhereHelperEnumarable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;
        private readonly Func<T, bool> _predicate;
        public WhereHelperEnumarable(IEnumerable<T> source, Func<T, bool> predicate)
        {
            _source = source;
            _predicate = predicate;
        }
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
