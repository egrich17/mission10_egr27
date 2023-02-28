using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Models.ViewModels
{
    public class PageInfo
    {
        // info I need for page number tracking
        public int TotalNumBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }

        // calculate total pages and cast variables for proper division calculation
        // add ceiling so that we have enough pages in case of decimals
        public int TotalPages => (int) Math.Ceiling((double) TotalNumBooks / BooksPerPage); 
    }
}
