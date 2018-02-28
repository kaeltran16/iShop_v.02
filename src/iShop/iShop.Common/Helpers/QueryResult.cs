using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Helpers
{
    public class QueryResult<T>
    {
        public int TotalItem { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
