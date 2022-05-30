using Microsoft.AspNetCore.Mvc;
using Xure.Api.Interfaces;
using Xure.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Xure.App.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Xure.App.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {        
        private UserManager<AppUser> UserManager { get; set; }
        private SignInManager<AppUser> SignInManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }
        private ICompanyRepository CompanyRepository { get; set; }
        private IUserRepository userRepository { get; set; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager
            ,IUserRepository repository, ICompanyRepository companyRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.roleManager = roleManager;
            userRepository = repository;
            CompanyRepository = companyRepository;
        }

        [AllowAnonymous]
        public IActionResult CreateClientAccount() => View();

        [AllowAnonymous]
        public IActionResult CreateSellerAccount() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClientAccount(UserViewModel model,string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                AppUser user = new AppUser() {

                    UserName = model.Name,
                    Surname = model.Surname,
                    MiddleName = model.MiddleName,
                    Birthday = model.Birthday,
                    PhoneNumber = model.PhoneNumber,
                    Passport = model.Passport,
                    Email = model.Email
                };                

                if (model.Avatar != null)
                {
                    byte[] data = null;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        data = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }

                    user.Avatar = data;
                }

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user, "Покупатель");
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSellerAccount(CreateSellerViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {

                    UserName = model.User.Name,
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
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel details,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await SignInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(returnUrl ?? "Profile", "Account");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Неверный логин или пароль");
            }
            return View(details);
        }
                
        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
