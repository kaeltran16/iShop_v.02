using System;
using System.Collections.Generic;
using System.Text;

namespace iShop.Common.Helpers
{
    public class QueryObject
    {
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
