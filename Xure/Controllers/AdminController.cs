using Xure.Data;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Xure.App.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Xure.App.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AdminController : Controller
    {
        private IOrderReportRepository OrderReportRepository { get; set; }
        private IOrderRepository OrderRepository { get; set; }
        private IOrderProductRepository OrderProductRepository { get; set; }
        private IProductRepository ProductRepository { get; set; }
        private IMessageRepository MessageRepository { get; set; }
        private IProductSpecificationsRepository ProductSpecificationsRepository { get; set; }
        private IProductReportRepository ProductReportRepository { get; set; }
        private IProductSpecificationsValueRepository productSpecificationsValueRepository { get; set; }

        private ICategoryRepository CategoryRepository { get; set; }
        private IUnitRepository unitRepository { get; set; }
        private IReceptionPointRepository receptionPointRepository { get; set; }
        private ISellerRepository sellerRepository { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private UserManager<AppUser> UserManager { get; set; }
        public AdminController(IOrderReportRepository orderReportRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository, IMessageRepository messageRepository
        , IProductRepository productRepository, IUnitRepository unitRepository, IProductSpecificationsValueRepository productSpecificationsValueRepository
            , IProductSpecificationsRepository productSpecificationsRepository, IProductReportRepository productReportRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ISellerRepository sellerRepository
            , ICategoryRepository categoryRepository, IReceptionPointRepository receptionPointRepository)
        {
            OrderReportRepository = orderReportRepository;
            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            MessageRepository = messageRepository;
            ProductRepository = productRepository;
            this.unitRepository = unitRepository;
            this.productSpecificationsValueRepository = productSpecificationsValueRepository;
            this.ProductSpecificationsRepository = productSpecificationsRepository;
            ProductReportRepository = productReportRepository;
            RoleManager = roleManager;
            UserManager = userManager;
            this.sellerRepository = sellerRepository;
            this.CategoryRepository = categoryRepository;
            this.receptionPointRepository = receptionPointRepository;
        }

        public IActionResult ReportList()
        {
            return View(OrderProductRepository.GetWithInclude(c => c.Status == "Подано заявление на возврат" && c.Order.Client.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            , c => c.Product, c => c.Product.Seller.UserInfo, c => c.Order.Client.UserInfo));
        }

        public IActionResult ReturnOrder(string answer)
        {

            return View();
        }

        public IActionResult ReportInfo(AdminViewModel report)
        {
            var orderReport = OrderReportRepository.GetWithInclude(c => c.Id == report.orderReport.Id, c => c.Order.Client.UserInfo, c => c.Product.Price.PriceHistory, c => c.Product.Seller.UserInfo).FirstOrDefault();
            ReportInfoViewModel vm = new ReportInfoViewModel
            {
                OrderReport = orderReport,
                Messages = MessageRepository.GetWithInclude(c => c.Sender.Id == orderReport.Order.Client.UserId || c.SenderId == orderReport.Product.Seller.UserId
                && c.Recipient.Id == orderReport.Order.Client.UserId || c.Recipient.Id == orderReport.Product.Seller.UserId, c => c.Sender, c => c.Recipient).OrderBy(c => c.MessageTime)
            };

            return View(vm);
        }

        public IActionResult ProductInfo(AdminViewModel model)
        {

            ProductReport report = ProductReportRepository.GetWithInclude(c => c.Id == model.productReport.Id, c => c.Product).FirstOrDefault();
            Product product2 = ProductRepository.GetWithInclude(c => c.Id == report.ProductId, c => c.Seller, c => c.Price.PriceHistory, c => c.Category, c => c.Brands, c => c.Seller.Company).FirstOrDefault();
            var vm = new ProductInfoViewModel
            {
                product = product2,
                Units = unitRepository.GetAll(),
                productSpecifications = ProductSpecificationsRepository.GetWithInclude(c => product2.Category.Id == c.CategoryId),
                productSpecificationsValues = productSpecificationsValueRepository.GetWithInclude(c => c.ProductId == product2.Id, c => c.Unit, c => c.ProductSpecification),
                ProductReport = report
            };
            return View(vm);
        }

        public IActionResult DeleteProductReport(int id)
        {

            ProductReportRepository.Delete(id);

            return RedirectToAction("AdminProfile");
        }

        public ViewResult Roles()
        {
            var vm = new RoleViewModel
            {
                roles = RoleManager.Roles,
                users = UserManager.Users
            };

            return View(vm);
        }

        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Роли не найдены");
            }
            return View("Roles", RoleManager.Roles);
        }
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonmembers = new List<AppUser>();
            foreach (AppUser user in UserManager.Users)
            {
                var list = await UserManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await UserManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    AppUser user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await UserManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Roles));
            }
            else
            {
                return await EditRole(model.RoleId);
            }
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> ClientList()
        {
            List<AppUser> clients = new List<AppUser>();

            foreach (var i in UserManager.Users)
            {
                if (await UserManager.IsInRoleAsync(i, "Покупатель"))
                {
                    clients.Add(i);
                }
            }
            return View(clients);
        }

        public IActionResult SellerList()
        {
            List<Sellers> sellers = sellerRepository.GetSellersWithInclude().ToList();

            return View(sellers);
        }

        public IActionResult OrderList(int id)
        {
            if (id == 1) return View(OrderProductRepository.GetWithInclude(c => c.Status == "В сборке", c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo));
            if (id == 2) return View(OrderProductRepository.GetWithInclude(c => c.Status == "Отправлен", c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo));
            if (id == 3) return View(OrderProductRepository.GetWithInclude(c => c.Status == "Прибыл", c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo));
            if (id == 4) return View(OrderProductRepository.GetWithInclude(c => c.Status == "Подтвержден", c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo));
            else return View(OrderProductRepository.GetWithInclude(c => c.Status == "Подан запрос на возврат" || c.Status == "Средства возвращены", c => c.Product.Seller.UserInfo, c => c.Product.Seller.Company, c => c.Order.Client.UserInfo));
        }

        public IActionResult ProductList(int id)
        {
            return View(ProductRepository.GetWithInclude(c => c.Seller.UserInfo, c => c.Price.PriceHistory, c => c.Category, c => c.Brands, c => c.Seller.Company));
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryModel { Categories = CategoryRepository.GetAll() });
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Create(model.Category);
                return RedirectToAction("CategoryList");
            }
            return View(model.Category);
        }
        public IActionResult EditCategory(int id)
        {
            return View(CategoryRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Update(category);
                return RedirectToAction("CategoryList");
            }
            return View(category);
        }

        public IActionResult DeleteCategory(int id)
        {
            if (CategoryRepository.GetById(id) == null) return View("Error");
            else CategoryRepository.Delete(id);
            return RedirectToAction("CategoryList");
        }

        public IActionResult ReceptionPointList()
        {
            return View(new ReceptionPointModel { receptionPoints = receptionPointRepository.GetAll() });
        }

        [HttpPost]
        public IActionResult EditReceptionPoint(ReceptionPoint receptionPoint)
        {
            if (ModelState.IsValid)
            {
                receptionPointRepository.Update(receptionPoint);
                return RedirectToAction("ReceptionPointList");
            }
            return View(receptionPoint);
        }

        [HttpPost]
        public IActionResult CreateReceptionPoint(ReceptionPointModel model)
        {
            if (ModelState.IsValid)
            {
                receptionPointRepository.Create(model.ReceptionPoint);
                return RedirectToAction("ReceptionPointList");
            }
            return View(model.ReceptionPoint);
        }

        public IActionResult EditReceptionPoint(int id)
        {
            return View(receptionPointRepository.GetById(id));
        }

        public IActionResult DeleteReceptionPoint(int id)
        {
            receptionPointRepository.Delete(id);
            return RedirectToAction("ReceptionPointList");
        }


        public IActionResult UnitList()
        {
            return View(new UnitModel { Units = unitRepository.GetAll() });
        }

        [HttpPost]
        public IActionResult EditUnit(Units unit)
        {
            if (ModelState.IsValid)
            {
                unitRepository.Update(unit);
                return RedirectToAction("UnitList");
            }
            return View(unit);
        }

        [HttpPost]
        public IActionResult CreateUnit(UnitModel model)
        {
            if (ModelState.IsValid)
            {
                unitRepository.Create(model.Unit);
                return RedirectToAction("UnitList");
            }
            return View(model.Unit);
        }

        public IActionResult EditUnit(int id)
        {
            return View(unitRepository.GetById(id));
        }

        public IActionResult DeleteUnit(int id)
        {
            unitRepository.Delete(id);
            return RedirectToAction("UnitList");
        }

        // 
        public IActionResult ProductSpecificationList()
        {
            return View(new ProductSpecificationsModel
            {
                ProductSpecifications = ProductSpecificationsRepository.GetWithInclude(C => C.Category).OrderBy(c => c.Category.Name),
                Categories = CategoryRepository.GetAll()
            });
        }

        [HttpPost]
        public IActionResult EditProductSpecification(ProductSpecifications productSpecification)
        {
            if (ModelState.IsValid)
            {
                ProductSpecificationsRepository.Update(productSpecification);
                return RedirectToAction("ProductSpecificationList");
            }
            return View(productSpecification);
        }

        [HttpPost]
        public IActionResult CreateProductSpecification(ProductSpecificationsModel model)
        {
            if (ModelState.IsValid)
            {
                ProductSpecificationsRepository.Create(model.ProductSpecification);
                return RedirectToAction("ProductSpecificationList");
            }
            return View(model.ProductSpecification);
        }

        public IActionResult EditProductSpecification(int id)
        {
            return View(new ProductSpecificationsModel
            {
                ProductSpecification = ProductSpecificationsRepository.GetById(id),
                Categories = CategoryRepository.GetAll()
            });
        }

        public IActionResult DeleteProductSpecification(int id)
        {
            ProductSpecificationsRepository.Delete(id);
            return RedirectToAction("ProductSpecificationList");
        }

    }
}
