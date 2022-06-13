using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using Xure.Data;
using Xure.App.Models;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xure.App.Infrastructure;
namespace Xure.App.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _productRepository;

        private Cart _cart;
        public CartController(IProductRepository productRepository, Cart cart)
        {
            _productRepository = productRepository;
            _cart = cart;
        }

        public IActionResult Index(string returnUrl)
        {
            var vm = new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        public RedirectToActionResult AddToCart(long Id, string returnUrl)
        {
            Product product = _productRepository.GetById(Id);
            if (product != null)
            {                
                _cart.AddItem(product, 1);                
            }
            return RedirectToAction("Index", new { returnUrl, Id });
        }

        public RedirectToActionResult RemoveFromCart(long productId, string returnUrl)
        {
            Product product = _productRepository.GetById(productId);
            if (product != null)
            {                
                _cart.RemoveLine(product);                
            }
            return RedirectToAction("Index", new { returnUrl });
        }       
    }
}
