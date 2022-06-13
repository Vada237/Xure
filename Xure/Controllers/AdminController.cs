using Xure.Data;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Xure.App.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AdminController : Controller
    {
        private IOrderReportRepository OrderReportRepository { get; set; }

        private IOrderRepository OrderRepository { get; set; }

        private IOrderProductRepository ProductRepository { get; set; }

        public AdminController(IOrderReportRepository orderReportRepository,IOrderRepository orderRepository,IOrderProductRepository orderProductRepository)
        {
            OrderReportRepository = orderReportRepository;
            OrderRepository = orderRepository;
            ProductRepository = orderProductRepository;
        }

        public IActionResult ReportList()
        {            
            return View(ProductRepository.GetWithInclude(c => c.Status == "Подано заявление на возврат" && c.Order.Client.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            , c => c.Product, c => c.Product.Seller.UserInfo, c => c.Order.Client.UserInfo));
        }

        public IActionResult ReturnOrder(string answer)
        {

            return View();
        }
             
    }
}
