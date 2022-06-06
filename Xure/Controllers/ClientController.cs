using Microsoft.AspNetCore.Mvc;
using System;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace Xure.App.Controllers
{
    public class ClientController : Controller
    {
        private IOrderProductRepository _orderProductRepository;
        private IClientRepository _clientRepository;
        public ClientController(IOrderProductRepository orderProductRepository, IClientRepository clientRepository)
        {
            _orderProductRepository = orderProductRepository;
            _clientRepository = clientRepository;
        }

        public IActionResult OrderList()
        {
            IEnumerable<OrderProduct> orderProducts = _orderProductRepository.GetWithInclude(c => c.Order.ClientId ==
            _clientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id && c.Order.Id == c.OrderId, c => c.Order.ReceptionPoint, c => c.Product.Brands, c => c.Product.Price.PriceHistory);
            return View(orderProducts);
        }
        

        public IActionResult OrderProductInfo(string orderId,string ProductId)
        { 
            return View(_orderProductRepository.GetWithInclude(c=> c.OrderId == long.Parse(orderId) && c.ProductId == long.Parse(ProductId),c => c.Order.ReceptionPoint,
                c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo).FirstOrDefault());
        }
    }
}
