using System.Collections.Generic;

namespace Application.Dtos.Utilities
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string Query { get; set; }
        public int TotalNumberOfItems { get; set; }

        public PagedResponse()
        {
        }

        public PagedResponse(PaginationQuery request, IEnumerable<T> data, int totalNumberOfItems)
        {
            PageNumber = request.PageNumber;
            PageSize = request.PageSize;
            Query = request.Query;

            TotalNumberOfItems = totalNumberOfItems;
            Data = data;
        }
    }
}