using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Utilities
{
    public class PaginationQuery
    {
        private int _pageNumber;

        private int _pageSize;

        public int PageNumber
        {
            get => _pageNumber == 0 ? 1 : _pageNumber;
            set
            {
                if (value < 1)
                    _pageNumber = 1;
                else
                    _pageNumber = value;
            }
        }

        public int PageSize
        {
            get => _pageSize == 0 ? 5 : _pageSize;
            set
            {
                if(value < 1)
                    _pageSize = 1;
                else if (value > 100)
                    _pageSize = 100;
                else
                    _pageSize = value;
            }
        }

        [MaxLength(255)]
        public string Query { get; set; }
    }
}