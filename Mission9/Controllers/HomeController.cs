using Microsoft.AspNetCore.Mvc;
using Mission9.Models;
using Mission9.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Controllers
{
    public class HomeController : Controller
    {
        // no longer need bc of repository
        //private BookstoreContext context { get; set; }

        //public HomeController (BookstoreContext temp)
        //{
        //    context = temp;
        //}

        // use abstraction to get data
        private IBookRepository repo;
        public HomeController (IBookRepository temp)
        {
            repo = temp;
        }
        // default index pageNum to 1
        public IActionResult Index(int pageNum = 1)
        {
            // want each page to show 10 results
            int pageSize = 10;

            // create new booksview model that we can pass in the index view
            var x = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = repo.Books.Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
        
            return View(x);
        }
    }
}
