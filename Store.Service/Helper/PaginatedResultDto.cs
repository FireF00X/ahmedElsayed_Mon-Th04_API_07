using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Helper
{
    public class PaginatedResultDto<T>
    {
        public PaginatedResultDto(int pageSize, int  pageIndex, int totalCount, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
            PageIndex = pageIndex;  
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount{  get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
