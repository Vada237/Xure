using Microsoft.AspNetCore.Mvc;
using System;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Xure.App.Models;

namespace Xure.App.Controllers
{
    public class ClientController : Controller
    {
        private IOrderProductRepository _orderProductRepository;
        private IClientRepository _clientRepository;
        private IProductRepository _productRepository;
        private IReviewsRepository _reviewsRepository;

        public ClientController(IOrderProductRepository orderProductRepository, IClientRepository clientRepository,IProductRepository productRepository
            ,IReviewsRepository reviewsRepository)
        {
            _orderProductRepository = orderProductRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _reviewsRepository = reviewsRepository;
        }

        public IActionResult OrderList()
        {
            IEnumerable<OrderProduct> orderProducts = _orderProductRepository.GetWithInclude(c => c.Order.ClientId ==
            _clientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id && c.Order.Id == c.OrderId, c => c.Product.Brands
            , c => c.Product.Price.PriceHistory, c => c.ReceptionPoint, c => c.Order, c=> c.Product.Reviews);
            return View(orderProducts);
        }
        

        public IActionResult OrderProductInfo(string orderId,string ProductId)
        { 
            return View(_orderProductRepository.GetWithInclude(c=> c.OrderId == long.Parse(orderId) && c.ProductId == long.Parse(ProductId),
                c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult CompleteOrder(OrderProduct orderProduct)
        {
            orderProduct = _orderProductRepository.GetByIds(orderProduct.OrderId, orderProduct.ProductId);
            orderProduct.Status = "Подтвержден";
            _orderProductRepository.Update(orderProduct);
            return RedirectToAction("OrderList");
        }
        
        public IActionResult CreateRating(int id)
        {
            return View(new RatingViewModel
            {
                Product = _productRepository.GetById(id),
                Client = _clientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            });
        }

        [HttpPost] 
        public IActionResult CreateReview(RatingViewModel model)
        {            
            _reviewsRepository.Create(model.Review);
            return RedirectToAction("Index", "Home");
        }
    }
}
