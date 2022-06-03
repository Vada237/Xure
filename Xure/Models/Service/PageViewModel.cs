using System;

namespace Xure.App.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public PageViewModel(int Count, int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            TotalPages = (int)Math.Ceiling(Count / (double)PageSize);
        }

        public bool HasPreviousPage
        {
            get { return (PageNumber > 1); }
        }

        public bool HasNextPage
        {
            get { return (PageNumber < TotalPages); }
        }
    }
}
