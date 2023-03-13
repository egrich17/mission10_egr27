using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Models
{
    // inherit from IPurchaseRepository
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private BookstoreContext context;
        public EFPurchaseRepository(BookstoreContext temp)
        {
            context = temp;
        }
        // access purchases: get data set for purchases and books
        public IQueryable<Purchase> Purchases => context.Purchase.Include(x => x.Lines).ThenInclude(x => x.Book);

        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Book));

            if (purchase.PurchaseId == 0)
            {
                context.Purchase.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
