// This file defines a DTO for paginated results, including metadata about the pagination.

using System;
using System.Collections.Generic;

namespace UserManagement.Core.DTOs
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } // The list of items in the current page
        public int TotalCount { get; set; } // The total number of items across all pages
        public int PageSize { get; set; } // The number of items per page
        public int CurrentPage { get; set; } // The current page number
        public int TotalPages { get; set; } // The total number of pages

        // Constructor to initialize the properties
        public PaginatedResult(List<T> items, int totalCount, int pageSize, int currentPage)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize); // Calculate total pages
        }
    }
}