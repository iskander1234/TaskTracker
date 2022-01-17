using System;
using TaskTracker.Controllers;

namespace TaskTracker.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public SortState SortState { get; set; }

        public PageViewModel(int pageNumber, int count, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}