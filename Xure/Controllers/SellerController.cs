using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xure.Api.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Xure.Data;
using Microsoft.AspNetCore.Identity;
using Xure.App.Models;

namespace Xure.App.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private UserManager<AppUser> UserManager { get; set; }
        private IProductRepository ProductRepository { get; set; }
        private ICategoryRepository CategoryRepository { get; set; }
        private IBrandRepository BrandRepository { get; set; }
        private IPriceRepository priceRepository { get; set; }
        private IPriceHistoryRepository priceHistoryRepository { get; set; }
        private IUnitRepository unitRepository { get; set; }
        private ISellerRepository sellerRepository { get; set; }
        private IProductSpecificationsRepository productSpecificationsRepository { get; set; }
        private IProductSpecificationsValueRepository productSpecificationsValueRepository { get; set; }
        
        private int PageSize = 10;
        public SellerController(UserManager<AppUser> userManager, IProductRepository productRepository,
            ICategoryRepository categoryRepository, IBrandRepository brandRepository, IPriceHistoryRepository priceHistoryRepository
            , IPriceRepository priceRepository, ISellerRepository sellerRepository, IUnitRepository unitRepository,
            IProductSpecificationsRepository productSpecificationsRepository, IProductSpecificationsValueRepository productSpecificationsValueRepository)
            {
                UserManager = userManager;
                ProductRepository = productRepository;
                CategoryRepository = categoryRepository;
                BrandRepository = brandRepository;
                this.priceHistoryRepository = priceHistoryRepository;
                this.priceRepository = priceRepository;
                this.sellerRepository = sellerRepository;
                this.unitRepository = unitRepository;
                this.productSpecificationsRepository = productSpecificationsRepository;
                this.productSpecificationsValueRepository = productSpecificationsValueRepository;
            }

            [Authorize(Roles = "Поставщик,НеподтвержденныйПоставщик")]
            public IActionResult ProductList()
            {
                ViewBag.BodyTitle = "Список товаров";
                var ViewModel = new ProductListViewModel
                {
                    Products = ProductRepository.FindProductBySeller(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    Categories = CategoryRepository.GetAll(),                    
                };

                return View(ViewModel);
            }

            [Authorize(Roles = "Поставщик,НеподтвержденныйПоставщик")]
            public IActionResult CreateProduct()
            {
                var ViewModel = new ProductViewModel()
                {
                    Brands = BrandRepository.GetAll(),
                    Categories = CategoryRepository.GetAll()
                };
                return View(ViewModel);
            }

            [HttpPost]
            public IActionResult CreateProduct(ProductViewModel model)
            {
                if (ModelState.IsValid)
                {
                    if (CategoryRepository.GetByName(model.Category.Name) == null)
                    {
                        CategoryRepository.Create(model.Category);
                    }
                    if (BrandRepository.GetByName(model.Brand.Name) == null)
                    {
                        BrandRepository.Create(model.Brand);
                    }
                    priceHistoryRepository.Create(model.PriceHistory);

                    priceRepository.Create(new Prices
                    {
                        PriceHistoryId = model.PriceHistory.Id
                    });

                    byte[] image = null;
                    using (var binaryReader = new BinaryReader(model.Product.Image.OpenReadStream()))
                    {
                        image = binaryReader.ReadBytes((int)model.Product.Image.Length);
                    }

                    Product product = new Product
                    {
                        Name = model.Product.Name,
                        Description = model.Product.Description,
                        PriceId = priceRepository.GetByHistoryId(model.PriceHistory.Id).Id,
                        BrandId = BrandRepository.GetByName(model.Brand.Name).Id,
                        CategoryId = CategoryRepository.GetByName(model.Category.Name).Id,
                        SellerId = sellerRepository.GetIdByUserId(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                        Сount = model.Product.Count,
                        Image = image
                    };

                    ProductRepository.Create(product);
                    return RedirectToAction("ProductList");
                }
                return View(model);
            }

            [HttpGet]
            public IActionResult FindProductByName(ProductListViewModel Model)
            {
                var ViewModel = new ProductListViewModel
                {
                    Products = ProductRepository.FindProductsByName(Model.productName),
                    Categories = CategoryRepository.GetAll()
                };
                if (ViewModel.Products.Count() == 0)
                {
                    ViewBag.BodyTitle = "Товары не найдены";
                } else
                {
                    ViewBag.BodyTitle = "Найдены следующие товары по запросу:";
                }
                return View("ProductList", ViewModel);
            }

            [HttpGet]
            public IActionResult FindProductsByCategory(ProductListViewModel Model)
            {
                var ViewModel = new ProductListViewModel
                {
                    Products = ProductRepository.FindProductByCategory(Model.CategoryName),
                    Categories = CategoryRepository.GetAll()
                };
                if (ViewModel.Products.Count() == 0)
                {
                    ViewBag.BodyTitle = "В выбранной категории нет товаров";
                } else
                {
                    ViewBag.BodyTitle = "Найдены следующие товары: ";
                }
                return View("ProductList", ViewModel);
            }

            public IActionResult ProductInfo(int id)
            {
                Product product2 = ProductRepository.GetById(id);
                var vm = new ProductInfoViewModel
                {
                    product = product2,
                    Units = unitRepository.GetAll(),
                    productSpecifications = productSpecificationsRepository.GetWithInclude(c => product2.Category.Id == c.CategoryId),
                    productSpecificationsValues = productSpecificationsValueRepository.GetWithInclude(c => c.ProductId == product2.Id, c=> c.Unit, c=> c.ProductSpecification)
                };
                return View(vm);
            }

            public IActionResult AddSpecification(int id)
            {
            Product product2 = ProductRepository.GetById(id);
            var vm = new ProductInfoViewModel
            {
                product = product2,
                productSpecifications = productSpecificationsRepository.GetWithInclude(c => c.CategoryId == product2.CategoryId),
                Units = unitRepository.GetAll()
                };
            return View(vm);
            }

            [HttpPost]
            public IActionResult AddSpecification(ProductInfoViewModel model) {            
            if (ModelState.ErrorCount < 3) {
                var ProductSpecificationValue = new ProductSpecificationsValue
                {                    
                    ProductId = model.product.Id,
                    ProductSpecificationsId = productSpecificationsRepository.GetByName(model.productSpecification.Name).Id,
                    Value = model.productSpecificationsValue.Value,
                    UnitId = unitRepository.GetByName(model.Unit.Name).id
                    };
                productSpecificationsValueRepository.Create(ProductSpecificationValue);
                return RedirectToAction("ProductList");
                }
                return View(model);
            }
        }
    }
