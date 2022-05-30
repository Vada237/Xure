using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Xure.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Xure.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]
    public class ApiAccountController : ControllerBase
    {
        private UserManager<AppUser> UserManager;
        private SignInManager<AppUser> SignInManager;

        public ApiAccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) 
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]        
        public async Task<IActionResult> Create (string returnUrl,CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Surname = model.Surname,
                    MiddleName = model.MiddleName,
                    Birthday = model.Birthday,                    
                    PhoneNumber = model.PhoneNumber,
                    Passport = model.Passport,
                    Email = model.Email
                };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) { 
                    return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + '/' + returnUrl, model);
                }

                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return BadRequest("Пользователь не создан");
        }

        [HttpPost]
        [AllowAnonymous]        

        public async Task<IActionResult> Login (LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await SignInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, model.Password ,false, false);
                    if (result.Succeeded)
                    {
                        return Ok("Пользователь авторизован");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Неверный логин или пароль");
            }
            return BadRequest();
        }
        
        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return Ok("Произведен выход из аккаунта");
        }       
    }
}
