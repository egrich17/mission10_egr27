using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission9.Infrastructure;
using Mission9.Models;

namespace Mission9.Pages
{
    public class BuyModel : PageModel
    {
        // create instance of our repository so we can pull book data
        private IBookRepository repo { get; set; }

        public BuyModel (IBookRepository temp)
        {
            repo = temp;
        }

        // create instance of basket
        public Basket basket { get; set; }

        // instance of return Url
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(b, 1);

            HttpContext.Session.SetJson("basket", basket);

            // redirect to our return url so that the user can continue shopping
            return RedirectToPage(new { ReturnUrl = returnUrl});
        }
    }
}
