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

        private IReasonRepository _reasonRepository;

        private IOrderReportRepository _orderReportRepository;
        private Cart Cart { get; set; }

        public OrderController(IOrderRepository orderRepository, ISellerRepository sellerRepository,IReceptionPointRepository receptionPointRepository,
            IProductRepository productRepository, ISellerOrderRepository sellerOrderRepository, IClientRepository clientRepository,
            IOrderProductRepository orderProductRepository,IReasonRepository reasonRepository,IOrderReportRepository reportRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            _sellerOrderRepository = sellerOrderRepository;
            _clientRepository = clientRepository;
            _sellerOrderRepository = sellerOrderRepository;
            _sellerRepository = sellerRepository;
            _receptionPointRepository = receptionPointRepository;
            this.orderProductRepository = orderProductRepository;
            this._reasonRepository = reasonRepository;
            this._orderReportRepository = reportRepository;
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

                if(Cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }
            
            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    ClientId = _clientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id
                };                
                
                _orderRepository.Create(order);

                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ReceptionPointId = model.Product.ReceptionPointId,
                    Status = model.Product.Status,
                    OrderDate = model.Product.OrderDate
                };

                var sellerOrder = new SellerOrder
                {
                    OrderId = order.Id
                };

                foreach (var line in Cart.Lines)
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
            
        public IActionResult returnOrder(OrderProduct orderProduct)
        {
            var vm = new reportOrderModel
            {
                orderProduct = orderProductRepository.GetByIds(orderProduct.OrderId,orderProduct.ProductId),
                Reasons = _reasonRepository.GetWithInclude(c => c.Category == "Заказ")
            };

            return View(vm);
        }

        public IActionResult CreateOrderReport(reportOrderModel model)
        {
            var orderReport = new OrderReport
            {
                OrderId = model.orderReport.OrderId,                
                ProductId = model.orderReport.ProductId,
                ReasonId = model.orderReport.ReasonId,
                Commentary = model.orderReport.Commentary,
                Status = "Активен"                
            };

            var orderProduct = orderProductRepository.GetByIds(orderReport.OrderId, orderReport.ProductId);
            orderProduct.Status = "Подан запрос на возврат";

            orderProductRepository.Update(orderProduct);

            _orderReportRepository.Create(orderReport);            

            return RedirectToAction("ReportList","Report");
        } 
    }
}
