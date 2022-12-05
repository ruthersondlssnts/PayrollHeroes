using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Enums;

namespace TPS.Infrastructure.Pagination
{
    public class PaginatedReturn<T> where T : class
    {
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        public int CurrentPage { get; set; }
        public string OrderByColumn { get; set; }
        public string OrderByASCOrDESC { get; set; }
        public List<T> DataList { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
