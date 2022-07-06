using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xure.Api.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Xure.Data;
using Microsoft.AspNetCore.Identity;
using Xure.App.Models;
using System;

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
        private ISellerOrderRepository sellerOrderRepository { get; set; }
        private IOrderRepository orderRepository { get; set; }
        private IOrderProductRepository orderProductRepository { get; set; }

        private IReviewsRepository reviewsRepository { get; set; }
        private IDeliveryRepository deliveryRepository { get; set; }

        private int PageSize = 10;
        public SellerController(UserManager<AppUser> userManager, IProductRepository productRepository,
            ICategoryRepository categoryRepository, IBrandRepository brandRepository, IPriceHistoryRepository priceHistoryRepository
            , IPriceRepository priceRepository, ISellerRepository sellerRepository, IUnitRepository unitRepository,
            IProductSpecificationsRepository productSpecificationsRepository, IProductSpecificationsValueRepository productSpecificationsValueRepository,
            ISellerOrderRepository sellerOrderRepository,IOrderProductRepository orderProductRepository,IOrderRepository orderRepository,
            IDeliveryRepository deliveryRepository, IReviewsRepository reviewsRepository)
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
                this.sellerOrderRepository = sellerOrderRepository;
                this.orderProductRepository = orderProductRepository;
                this.orderRepository = orderRepository;
                this.deliveryRepository = deliveryRepository;
                this.reviewsRepository = reviewsRepository;
            }

            [Authorize(Roles = "Поставщик,НеподтвержденныйПоставщик")]
            public IActionResult ProductList()
            {
                ViewBag.BodyTitle = "Список товаров";
            var ViewModel = new ProductListViewModel
            {
                Products = ProductRepository.FindProductBySeller(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Brands = BrandRepository.GetAll(),
                Categories = CategoryRepository.GetAll(),
                productSpecifications = productSpecificationsRepository.GetAll().Distinct()
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
                    Products = ProductRepository.FindProductsByName(Model.product.Name),
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
            public IActionResult FindProducts(string? productName, string? categoryName, string? brandName, string? minPrice, string? maxPrice, string? productSpecifications)
            {
            var ViewModel = new ProductListViewModel
            {
                Products = ProductRepository.FindProducts(productName, categoryName, brandName, minPrice, maxPrice, productSpecifications),
                Brands = BrandRepository.GetAll(),
                Categories = CategoryRepository.GetAll(),
                productSpecifications = productSpecificationsRepository.GetAll().Distinct()
            };                
           

                if (ViewModel.Products.Count() == 0)
                {
                    ViewBag.BodyTitle = "Товары не найдены";
                } else
                {
                    ViewBag.BodyTitle = "Найдены следующие товары: ";
                }
                return View("ProductList", ViewModel);
            }

            public IActionResult ProductInfo(int id)
            {
                Product product2 = ProductRepository.GetWithInclude(c => c.Id == id,c => c.Seller,c => c.Price.PriceHistory, c => c.Category, c => c.Brands).FirstOrDefault();
                var vm = new ProductInfoViewModel
                {                    
                    product = product2,
                    Units = unitRepository.GetAll(),
                    productSpecifications = productSpecificationsRepository.GetWithInclude(c => product2.Category.Id == c.CategoryId),
                    productSpecificationsValues = productSpecificationsValueRepository.GetWithInclude(c => c.ProductId == product2.Id, c=> c.Unit, c=> c.ProductSpecification)
                };
                if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value == product2.Seller.UserId) vm.locked = false;
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

            public IActionResult DeleteProduct(long id)
            {           
            Product product = ProductRepository.GetAll().FirstOrDefault(c => c.Id == id);
            if (product != null) { 
                ProductRepository.Delete(id);
            }
            return RedirectToAction("ProductList");
            }

            [HttpPost]
            public IActionResult EditCountProduct(ProductInfoViewModel model)
            {
                Product product = ProductRepository.GetById(model.product.Id);
                product.Сount = model.product.Сount;
                ProductRepository.Update(product);
                return RedirectToAction("ProductList");
            }

            public IActionResult SellerOrderList()
            {
            var sellerId = sellerRepository.GetIdByUserId(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var vm = new SellerOrderViewModel
            {
                OrderProducts = orderProductRepository.GetWithInclude(
                    c => c.Product.Seller.Id == sellerId, c => c.Product.Brands, c => c.Product.Seller.UserInfo
                    , c => c.Product.Price.PriceHistory)
            };
               return View(vm);
            }

            public IActionResult OrderProductInfo(string productId, string orderId)
            {

            var vm = new SellerOrderViewModel
            {
                sellerOrder = sellerOrderRepository.GetWithOrderIdAndProductId(long.Parse(orderId), long.Parse(productId)),
                Product = orderProductRepository.GetByIds(long.Parse(orderId), long.Parse(productId)),
                order = orderRepository.GetWithInclude(c => c.Id == orderProductRepository.GetByIds(long.Parse(orderId), long.Parse(productId)).OrderId
                , c => c.Client.UserInfo).FirstOrDefault(),                   
                };
            return View(vm);
            }            

            [HttpPost]
            public IActionResult CreateDelivery(SellerOrderViewModel model)
            {
            if (ModelState.ErrorCount < 4) {
                
                var delivery = new Delivery
                {
                    Address = model.Delivery.Address,
                    ArrivalTime = model.Delivery.ArrivalTime,
                    DepartTime = model.Delivery.DepartTime,
                    ReceprtionPointId = model.Delivery.ReceprtionPointId
                    };                

                deliveryRepository.Create(delivery);

                var sellerOrder = sellerOrderRepository.GetSellerOrder(model.sellerOrder.Id);
                sellerOrder.DeliveryId = delivery.Id;
                sellerOrderRepository.Update(sellerOrder);

                var product = ProductRepository.GetById(model.Product.ProductId);
                product.Сount -= model.Product.Quantity;
                ProductRepository.Update(product);
                
                orderProductRepository.Update(model.Product);

                return RedirectToAction("Profile","Account");
            }
                return View(model);
            }
        
            public IActionResult SellerOrderDeliveryList()
            {
            var sellerId = sellerRepository.GetIdByUserId(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var vm = new SellerOrderViewModel
            {
                OrderProducts = orderProductRepository.GetWithInclude(
                    c => c.Product.Seller.Id == sellerId && c.Status == "В сборке", c => c.Product.Brands, c => c.Product.Seller.UserInfo
                    , c => c.Product.Price.PriceHistory)
            };

            return View("SellerOrderList", vm);
            }

            public IActionResult SellerReviewList()
            {
            return View(reviewsRepository.GetWithInclude(c => c.Product.Seller.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            , c => c.Client.UserInfo, c => c.Product.Seller.UserInfo));
            }
        }
    }
