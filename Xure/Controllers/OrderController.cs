using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;
using Xure.Data;
using Xure.App.Models;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Xure.App.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;

        private ISellerOrderRepository _sellerOrderRepository;

        private IOrderProductRepository orderProductRepository;

        private ISellerRepository _sellerRepository;

        private IClientRepository _clientRepository;

        private IReceptionPointRepository _receptionPointRepository;

        private Cart Cart { get; set; }

        public OrderController(IOrderRepository orderRepository, ISellerRepository sellerRepository,IReceptionPointRepository receptionPointRepository,
            IProductRepository productRepository, ISellerOrderRepository sellerOrderRepository, IClientRepository clientRepository,
            IOrderProductRepository orderProductRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            _sellerOrderRepository = sellerOrderRepository;
            _clientRepository = clientRepository;
            _sellerOrderRepository = sellerOrderRepository;
            _sellerRepository = sellerRepository;
            _receptionPointRepository = receptionPointRepository;
            this.orderProductRepository = orderProductRepository;

            Cart = cart;
        }

        public IActionResult Checkout()
        {
            var vm = new OrderViewModel
            {
                receptionPoints = _receptionPointRepository.GetAll(),                
                Order = new Order()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel model)
        {
            Cart cart = GetCart();            

            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }
            
            if (ModelState.IsValid)
            {
                model.Order.ClientId = _clientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id;
                
                _orderRepository.Create(model.Order);

                var orderProduct = new OrderProduct
                {
                    OrderId = model.Order.Id
                };

                var sellerOrder = new SellerOrder
                {
                    OrderId = model.Order.Id
                };

                foreach (var line in cart.Lines)
                {
                    orderProduct.ProductId = line.Product.Id;
                    orderProduct.Quantity = line.Quantity;
                    orderProductRepository.Create(orderProduct);
                }

                _sellerOrderRepository.Create(sellerOrder);

                return RedirectToAction(nameof(Completed));
            }
            else return View(model);
        }

        public ViewResult Completed()
        {
            Cart.Clear();
            return View();
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart");
            return cart;
        }        
    }
}
