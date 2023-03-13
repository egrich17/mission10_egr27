using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Models
{
    // use interface instead of class to make it a template
    public interface IPurchaseRepository
    {
        public IQueryable<Purchase> Purchases { get; }

        public void SavePurchase(Purchase purchase);
    }
}
