using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TPS.Infrastructure.Pagination
{
    public class PaginatedRequest
    {
        const int maxPageSize = 50;
        public string EmployeeId { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
        public string SearchKeyword { get; set; } = "";
        public string OrderByColumn { get; set; }
        public string OrderByASCOrDESC { get; set; }
    }

}
