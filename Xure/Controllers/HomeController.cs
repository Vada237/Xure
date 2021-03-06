using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xure.App.Models;
using Xure.Data;
using Xure.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Xure.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IProductSpecificationsRepository _productSpecificationsRepository;
        private readonly IProductSpecificationsValueRepository productSpecificationsValueRepository;
        private readonly IReviewsRepository _reviewsRepository;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, IProductRepository repository
            , ICategoryRepository categoryRepository, IBrandRepository brandRepository, IUnitRepository unitRepository,
            IProductSpecificationsRepository productSpecificationsRepository, IProductSpecificationsValueRepository productSpecificationsValueRepository
            , UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IReviewsRepository reviewsRepository)
        {
            _productRepository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _unitRepository = unitRepository;
            _productSpecificationsRepository = productSpecificationsRepository;
            this.productSpecificationsValueRepository = productSpecificationsValueRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _reviewsRepository = reviewsRepository;
        }

        public IActionResult Index()
        {
            var vm = new ProductMainViewModel()
            {
                Categories = _categoryRepository.GetAll(),
                Brands = _brandRepository.GetAll(),
                NewProducts = _productRepository.GetWithInclude(c => c.Seller.Id == c.SellerId
                , c => c.Seller.Company, c => c.Category, c => c.Brands, c => c.Price.PriceHistory)
                .OrderByDescending(c => c.Seller.Company.DateRegistration).Take(6)
                ,
            };
            
                
            
            return View(vm);                       
        }

        public IActionResult ProductInfo(int id)
        {
            Product product2 = _productRepository.GetWithInclude(c => c.Id == id, c => c.Seller.Company, c => c.Category, c=> c.Brands, c=> c.Price.PriceHistory).FirstOrDefault();
            var vm = new ProductInfoViewModel
            {
                product = product2,
                Units = _unitRepository.GetAll(),
                productSpecifications = _productSpecificationsRepository.GetWithInclude(c => product2.Category.Id == c.CategoryId),
                productSpecificationsValues = productSpecificationsValueRepository.GetWithInclude(c => c.ProductId == product2.Id, c => c.Unit, c => c.ProductSpecification),
                Reviews = _reviewsRepository.GetWithInclude(c => c.ProductId == product2.Id, c => c.Client.UserInfo)
            };            
            return View(vm);
        }

        [HttpGet]
        public IActionResult FindProducts(string? productName, string? categoryName, string? brandName, string? minPrice, string? maxPrice, string? productSpecifications)
        {
            var ViewModel = new ProductMainViewModel
            {
                AllProducts = _productRepository.FindProducts(productName, categoryName, brandName, minPrice, maxPrice, productSpecifications),
                Brands = _brandRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
            };


            if (ViewModel.AllProducts.Count() == 0)
            {
                ViewBag.BodyTitle = "Товары не найдены";
            }
            else
            {
                ViewBag.BodyTitle = "Найдены следующие товары: ";
            }
            return View("FindProducts", ViewModel);
        }
    }
}
