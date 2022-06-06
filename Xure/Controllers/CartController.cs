using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using Xure.Data;
using Xure.App.Models;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Xure.App.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _productRepository;

        private Cart _cart;
        public CartController(IProductRepository productRepository,Cart cart)
        {
            _productRepository = productRepository;
            _cart = cart;
        }

        public IActionResult Index(string returnUrl)
        {
            var vm = new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        public RedirectToActionResult AddToCart(long productId,string returnUrl)
        {
            Product product = _productRepository.GetById(productId);
            if (product != null)
            {
                _cart = GetCart();
                _cart.AddItem(product, 1);
                SaveCart(_cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(long productId, string returnUrl)
        {
            Product product = _productRepository.GetById(productId);
            if (product != null)
            {
                _cart = GetCart();
                _cart.RemoveLine(product);
                SaveCart(_cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJson<T>(this ISession session,string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
