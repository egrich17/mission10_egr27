using Microsoft.AspNetCore.Mvc;
using Mission9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchaseRepository repo { get; set; }
        public Basket basket { get; set; }
        public PurchaseController(IPurchaseRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }
        // hand in a purchase object
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }

        // method when user clicks the submit button at checkout
        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            List<BasketLineItem> items = basket.Items;
            if (items.Count() == 0) {
                ModelState.AddModelError("", "Sorry your basket is empty!");
            }

            if (ModelState.IsValid)
            {
                purchase.Lines = basket.Items.ToArray();
                repo.SavePurchase(purchase);
                basket.ClearBasket();
                return RedirectToPage("/PurchaseCompleted");
            }
            else
            {
                return View();
            }
        }
    }
}
