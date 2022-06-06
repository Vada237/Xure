using Microsoft.AspNetCore.Mvc;
using Xure.Data;

namespace Xure.App.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartservice)
        {
            cart = cartservice;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
