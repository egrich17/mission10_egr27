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
        // create instance of basket
        public Basket basket { get; set; }
        public BuyModel (IBookRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        // instance of return Url
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            basket.AddItem(b, 1);

            // redirect to our return url so that the user can continue shopping
            return RedirectToPage(new { ReturnUrl = returnUrl});
        }
        // when "delete" form is submitted, update our basket and redirect user to returnUrl that is passed in
        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
