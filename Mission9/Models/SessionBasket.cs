using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission9.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission9.Models
{
    public class SessionBasket : Basket
    {
        // method to get a basket
        // if a session basket has already been started, us it. otherwise create a new session basket
        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        // create a method to override the add item method
        public override void AddItem(Book book, int qty)
        {
            base.AddItem(book, qty);
            Session.SetJson("Basket", this);
        }

        // override remove item
        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);
        }

        // override clear
        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }
    }
}
