using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Xure.App.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Xure.App.Infrastructure;
using System.Diagnostics;

namespace Xure.App.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserManager { get; set; }
        private SignInManager<AppUser> SignInManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }
        private ICompanyRepository CompanyRepository { get; set; }
        private ISellerRepository SellerRepository { get; set; }
        private ISellerOrderRepository sellerOrderRepository { get; set; }
        private IOrderRepository OrderRepository { get; set; }
        private IClientRepository ClientRepository { get; set; }
        private IOrderProductRepository orderProductRepository { get; set; }
        private IProductReportRepository ProductReportRepository { get; set; }
        private IOrderReportRepository OrderReportRepository { get; set; }
        private IUnitRepository UnitRepository { get; set; }
        private IProductSpecificationsValueRepository ProductSpecificationsValueRepository { get; set; }

        private IProductSpecificationsRepository ProductSpecificationsRepository { get; set; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager
            , ICompanyRepository companyRepository, ISellerRepository sellerRepository, IClientRepository clientRepository
            , ISellerOrderRepository sellerOrderRepository, IOrderRepository orderRepository, ICategoryRepository categoryRepository,
            IOrderProductRepository orderProductRepository, IOrderReportRepository orderReportRepository, IProductReportRepository productReportRepository,
            IUnitRepository unitRepository, IProductSpecificationsRepository productSpecificationsRepository, IProductSpecificationsValueRepository productSpecificationsValueRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.roleManager = roleManager;
            CompanyRepository = companyRepository;
            SellerRepository = sellerRepository;
            ClientRepository = clientRepository;
            OrderRepository = orderRepository;
            this.sellerOrderRepository = sellerOrderRepository;
            this.orderProductRepository = orderProductRepository;
            this.OrderReportRepository = orderReportRepository;
            ProductReportRepository = productReportRepository;
            UnitRepository = unitRepository;
            ProductSpecificationsRepository = productSpecificationsRepository;
            ProductSpecificationsValueRepository = productSpecificationsValueRepository;
        }

        [AllowAnonymous]
        public IActionResult CreateClientAccount() => View();

        [AllowAnonymous]
        public IActionResult CreateSellerAccount() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClientAccount(CreateClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user, "Покупатель");

                    ClientRepository.Create(new Clients
                    {
                        UserId = user.Id
                    });

                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>Ссылка</a>");

                    return View("Confirm");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await UserManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSellerAccount(CreateSellerViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.User.Email,
                    Name = model.User.Name,
                    Surname = model.User.Surname,
                    MiddleName = model.User.MiddleName,
                    Birthday = model.User.Birthday,
                    PhoneNumber = model.User.PhoneNumber,
                    Passport = model.User.Passport,
                    Email = model.User.Email
                };

                Company company = new Company()
                {
                    Name = model.Company.Name,
                    Description = model.Company.Description,
                    DateRegistration = model.Company.DateRegistration,
                    INN = model.Company.INN,
                    OGRN = model.Company.OGRN
                };

                if (model.User.Avatar != null)
                {
                    byte[] data = null;
                    using (var binaryReader = new BinaryReader(model.User.Avatar.OpenReadStream()))
                    {
                        data = binaryReader.ReadBytes((int)model.User.Avatar.Length);
                    }

                    user.Avatar = data;
                }

                IdentityResult result = await UserManager.CreateAsync(user, model.User.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user, "НеподтвержденныйПоставщик");
                    CompanyRepository.Create(company);
                    SellerRepository.Create(new Sellers
                    {
                        UserId = user.Id,
                        CompanyId = company.Id
                    });

                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.User.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>Ссылка</a>");

                    return View("Confirm");

                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userId = user.Id,
                    code = code
                }
                , protocol: HttpContext.Request.Scheme);

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Сброс пароля", $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>Ссылка для сброса пароля</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return View("ResetPasswordConfirmation");
                }
                var result = await UserManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByEmailAsync(details.Email);

                if (user != null)
                {
                    await SignInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction("LoginTwoStep", "Account", new
                        {
                            userId = user.Id
                        });
                    }

                    if (result.Succeeded)
                    {
                        return RedirectToAction(returnUrl ?? "Profile", "Account");
                    }
                }

                ModelState.AddModelError(nameof(LoginModel.Email), "Неверный логин или пароль");
            }
            return View(details);
        }

        [AllowAnonymous]
        [HttpGet]        
        public async Task<IActionResult> LoginTwoStep(string userId)
        {            
            var user = await UserManager.FindByIdAsync(userId);
            var authentificatorKey = await UserManager.GetAuthenticatorKeyAsync(user);
            return View("LoginTwoStep", new LoginTwoStepModel { Token = authentificatorKey });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginTwoStep(LoginTwoStepModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await SignInManager.TwoFactorAuthenticatorSignInAsync(model.Code, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(model.ReturnUrl ?? "Profile", "Account");
                }
            }
            ModelState.AddModelError("", "Неверный код");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentificatonSetupPage()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var authentificatorKey = await UserManager.GetAuthenticatorKeyAsync(user);
                if (authentificatorKey == null)
                {
                    await UserManager.ResetAuthenticatorKeyAsync(user);
                    authentificatorKey = await UserManager.GetAuthenticatorKeyAsync(user);
                }
                return View(new TwoFactorAuthentificatorModel { AuthenticatorKey = authentificatorKey });
            }
        }       

        [HttpPost]
        public async Task<IActionResult> TwoFactorAuthentificatonSetupPage(TwoFactorAuthentificatorModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.GetUserAsync(User);
                bool isValidCode = await UserManager.VerifyTwoFactorTokenAsync(user, UserManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);
                if (isValidCode)
                {
                    if (user.TwoFactorEnabled == false) { 
                    await UserManager.SetTwoFactorEnabledAsync(user, true);
                    } else
                    {
                    await UserManager.SetTwoFactorEnabledAsync(user, false);
                    }
                    return RedirectToAction("Profile", "Account");
                }                
            }
            ModelState.AddModelError("", "Неверный код");
            return View(model);
        }

        public IActionResult Profile()
        {
            if (User.IsInRole("Администратор"))
            {
                return RedirectToAction("AdminProfile", "Account");
            }

            var vm = new ProfileViewModel
            {
                Seller = SellerRepository.GetSellersWithInclude().Where(c => c.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault(),
                Client = ClientRepository.GetClientWithInclude(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                SellerForOrders = sellerOrderRepository.GetOrders()
                .Where(C => C.Order.OrderProducts == C.Order.OrderProducts.Where(c => c.Product.Seller.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)),
            };
            if (vm.Seller != null)
            {
                vm.CountSellerOrders = orderProductRepository.GetWithInclude(
                    c => c.Product.Seller.Id == SellerRepository.GetIdByUserId(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), c => c.Product.Seller).Count();
            };
            return View(vm);
        }

        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> AdminProfile()
        {

            string roleName = "НеподтвержденныйПоставщик";
            IdentityRole role = await roleManager.FindByNameAsync(roleName);

            var vm = new AdminViewModel
            {
                Admin = await UserManager.GetUserAsync(User),
                sellers = new List<Sellers>(),
                orderReports = OrderReportRepository.GetWithInclude(c => c.Status == "Активен"
                , c => c.Order.Client.UserInfo, c => c.Product.Seller.UserInfo, c => c.Product.Price.PriceHistory),
                productReports = ProductReportRepository.GetWithInclude(c => c.Product.Category, c => c.Product.Price.PriceHistory, c => c.Product.Seller.UserInfo
                , c => c.Reason, c => c.Product.Category.ProductSpecifications, C => C.Product.ProductSpecificationsValues),
                Units = UnitRepository.GetAll()
            };

            if (role != null)
            {
                foreach (var seller in SellerRepository.GetWithInclude(C => C.UserInfo, c => c.Company))
                {
                    if (User != null && await UserManager.IsInRoleAsync(seller.UserInfo, role.Name))
                    {
                        vm.sellers.Add(seller);
                    }
                }
            }

            return View(vm);
        }

        public async Task<IActionResult> ConfirmSeller(int id)
        {
            var seller = await UserManager.FindByIdAsync(SellerRepository.GetWithInclude(c => c.Id == id, c => c.UserInfo, c => c.Company).FirstOrDefault().UserInfo.Id);

            if (seller != null)
            {
                IdentityResult result = await UserManager.RemoveFromRoleAsync(seller, "НеподтвержденныйПоставщик");
                result = await UserManager.AddToRoleAsync(seller, "Поставщик");
            }
            return RedirectToAction("AdminProfile");
        }


        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        public async Task<IActionResult> ProfileSettings()
        {
            var user = await UserManager.GetUserAsync(User);

            return View(new EditProfileViewModel
            {
                User = user
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user1 = await UserManager.GetUserAsync(User);

                user1.Name = model.User.Name;
                user1.Surname = model.User.Surname;

                if (User.IsInRole("Поставщик") || User.IsInRole("НеподтвержденныйПоставщик"))
                {
                    user1.MiddleName = model.User.MiddleName;
                    user1.Birthday = model.User.Birthday;
                    user1.Passport = model.User.Passport;

                    if (model.Avatar != null)
                    {
                        byte[] data = null;
                        using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                        {
                            data = binaryReader.ReadBytes((int)model.Avatar.Length);
                        }

                        user1.Avatar = data;
                    }
                }

                var result = await UserManager.UpdateAsync(user1);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "Account");
                }
                return View("ProfileSettings", model);
            }
            return View("ProfileSettings", model);
        }
    }
}
