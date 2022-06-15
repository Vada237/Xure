using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xure.Data;
using Xure.Api.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Xure.App.Models;
using System.Security.Claims;
using System;
using System.IO;

namespace Xure.App.Controllers
{
    public class ReportController : Controller
    {
        private IOrderReportRepository _orderReportRepository;

        private IMessageRepository _messageRepository;

        private IOrderProductRepository _orderProductRepository;

        private ISellerRepository _sellerRepository;

        private IClientRepository _clientRepository;

        private IUserRepository _userRepository;

        private IMessageRepository messageRepository;

        private IReasonRepository reasonRepository;

        private IProductRepository productRepository;

        private IProductReportRepository productReportRepository;

        public ReportController(IOrderReportRepository orderReportRepository,IMessageRepository repository,IOrderProductRepository orderProduct,ISellerRepository sellerRepository
            ,IClientRepository clientRepository, IUserRepository userRepository,IMessageRepository messageRepository,IReasonRepository reasonRepository,IProductRepository productRepository
            ,IProductReportRepository productReportRepository)
        {
            this._orderReportRepository = orderReportRepository;
            this._messageRepository = repository;
            this._orderProductRepository = orderProduct;
            this._sellerRepository = sellerRepository;
            this._clientRepository = clientRepository;  
            this._userRepository = userRepository;
            this.messageRepository = messageRepository;
            this.reasonRepository = reasonRepository;
            this.productRepository = productRepository;
            this.productReportRepository = productReportRepository; 
        }

        public IActionResult ReportList()
        {
            var vm = new ReportViewModel
            {
                orderReports = _orderReportRepository.GetWithInclude(c => c.Order.Client.Id == _clientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id
                , c => c.Order.Client.UserInfo, c => c.Reason, c => c.Product, c => c.Order.Products)
            };
            return View(vm);
        }

        public IActionResult ReportInfo(ReportViewModel model, string? orderId, string? productId)
        {
            
            var vm = new ReportInfoViewModel();
            
            if (User.IsInRole("Покупатель")) {

                var orderReport = _orderReportRepository.GetWithInclude(c => c.Id == model.OrderReport.Id
                , c => c.Order.Client.UserInfo, c => c.Product.Seller.UserInfo, c => c.Product.Price.PriceHistory).FirstOrDefault();

                    vm.OrderReport = orderReport;

                    vm.Messages = _messageRepository.GetWithInclude(c => c.SenderId == orderReport.Order.Client.UserId && c.RecipientId == orderReport.Product.Seller.UserId
                    || c.RecipientId == orderReport.Order.Client.UserId && c.SenderId == orderReport.Product.Seller.UserId
                , c => c.Sender, c => c.Recipient).OrderBy(c => c.MessageTime);
                vm.Sender = _clientRepository.GetWithInclude(c => c.UserInfo.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, c => c.UserInfo).FirstOrDefault().UserInfo;               
            }
            if(User.IsInRole("Поставщик") || User.IsInRole("НеподтвержденныйПоставщик"))
            {
                var orderReport = _orderReportRepository.GetWithInclude(c => c.OrderId == long.Parse(orderId) && c.ProductId == long.Parse(productId),
                   c => c.Order.Client.UserInfo, c => c.Product.Seller.UserInfo, c => c.Product.Price.PriceHistory).FirstOrDefault();

                vm.OrderReport = orderReport;

                vm.Messages = _messageRepository.GetWithInclude(c => c.SenderId == orderReport.Order.Client.UserId && c.RecipientId == orderReport.Product.Seller.UserId
                || c.RecipientId == orderReport.Order.Client.UserId && c.SenderId == orderReport.Product.Seller.UserId
                , c => c.Sender, c => c.Recipient).OrderBy(c => c.MessageTime);
                vm.Sender = _sellerRepository.GetWithInclude(c => c.UserInfo.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, c => c.UserInfo).FirstOrDefault().UserInfo;
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddMessage(ReportInfoViewModel model)
        {
            if (ModelState.IsValid)
            {

                var orderReport = _orderReportRepository.GetWithInclude(c => c.Id == model.OrderReport.Id
                , C => C.Order.Client.UserInfo, c => c.Product.Seller.UserInfo, C => C.Product.Price.PriceHistory).FirstOrDefault();


                var message = new Message
                {
                    Text = model.Message.Text,                    
                    MessageTime = DateTime.Now,
                    SenderId = _userRepository.GetUserById(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Id,
                };

                if (model.Img != null)
                {
                    byte[] data = null;
                    using (var binaryReader = new BinaryReader(model.Img.OpenReadStream()))
                    {
                        data = binaryReader.ReadBytes((int)model.Img.Length);
                    }

                    message.Image = data;
                }

                if (User.IsInRole("Покупатель"))
                {
                    message.RecipientId = orderReport.Product.Seller.UserId;
                }

                if (User.IsInRole("Поставщик") || User.IsInRole("НеподтвержденныйПоставщик"))
                {
                    message.RecipientId = orderReport.Order.Client.UserId;
                }

                messageRepository.Create(message);

                var vm = new ReportInfoViewModel();

                if (User.IsInRole("Покупатель"))
                {                    

                    vm.OrderReport = orderReport;

                    vm.Messages = _messageRepository.GetWithInclude(c => c.SenderId == orderReport.Order.Client.UserId && c.RecipientId == orderReport.Product.Seller.UserId
                    || c.RecipientId == orderReport.Order.Client.UserId && c.SenderId == orderReport.Product.Seller.UserId
                , c => c.Sender, c => c.Recipient).OrderBy(c => c.MessageTime);
                    vm.Sender = _clientRepository.GetWithInclude(c => c.UserInfo.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, c => c.UserInfo).FirstOrDefault().UserInfo;
                }
                if (User.IsInRole("Поставщик") || User.IsInRole("НеподтвержденныйПоставщик"))
                {                    

                    vm.OrderReport = orderReport;

                    vm.Messages = _messageRepository.GetWithInclude(c => c.SenderId == orderReport.Order.Client.UserId && c.RecipientId == orderReport.Product.Seller.UserId
                    || c.RecipientId == orderReport.Order.Client.UserId && c.SenderId == orderReport.Product.Seller.UserId
                    , c => c.Sender, c => c.Recipient).OrderBy(c => c.MessageTime);
                    vm.Sender = _sellerRepository.GetWithInclude(c => c.UserInfo.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, c => c.UserInfo).FirstOrDefault().UserInfo;
                }

                return View("ReportInfo",vm);
            }
            return View(model);            
        }
        [HttpPost]
        public IActionResult ReturnMoney(ReportInfoViewModel model)
        {
            var OrderProduct = _orderProductRepository.GetByIds(model.OrderReport.OrderId, model.OrderReport.ProductId);
            OrderProduct.Status = "Средства возвращены";
            _orderProductRepository.Update(OrderProduct);

            var orderReport = _orderReportRepository.GetWithInclude(c => c.OrderId == model.OrderReport.OrderId && c.ProductId == model.OrderReport.ProductId).FirstOrDefault();
            orderReport.Status = "Завершен";
            _orderReportRepository.Update(orderReport);
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ReportProduct(ProductInfoViewModel model)
        {
            var vm = new ReportProductViewModel
            {
                Reasons = reasonRepository.GetWithInclude(c => c.Category == "Товар"),
                Product = productRepository.GetById(model.product.Id)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateOrderProduct(ReportProductViewModel model)
        {
            productReportRepository.Create(model.ProductReport);

            return RedirectToAction("Index","Home");
        }
    }
}
