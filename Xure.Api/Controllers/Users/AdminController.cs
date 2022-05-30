using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Xure.Data;
using System.Threading.Tasks;
using Xure.Api.Interfaces;
namespace Xure.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Администратор")]
    public class ApiAdminController : ControllerBase
    {
        private UserManager<AppUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private IUserRepository UserRepository;
        public ApiAdminController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IUserRepository userRepository)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            UserRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return Ok($"Создана роль: {model.Name}");
                }
            }
            return BadRequest("Введена не корректная роль");
        }

        [HttpPut]
        [Route("RoleName={name}")]
        public async Task<IActionResult> UpdateRole(RoleModel model,string name)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await RoleManager.FindByNameAsync(name);
                IdentityResult result = await RoleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Ok("Роль изменена");
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("RoleName={name}")]
        public async Task<IActionResult> DeleteRole(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await RoleManager.FindByNameAsync(name);
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok("Роль удалена");
                }
            }
            return BadRequest();
        }            

        [HttpPost]
        [Route("{email}")]
        public async Task<IActionResult> SetUserRoleByEmail(RoleModel model,string Email) 
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByEmailAsync(Email);
                IdentityRole role = await RoleManager.FindByNameAsync(model.Name);

                if (user != null && role != null)
                {
                    IdentityResult result = await UserManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        return Ok($"Пользователь: {user.Surname} {user.UserName}\nEmail: {user.Email} \nРоль: {role.Name}");
                    }
                }
                return NotFound("Пользователь или роль не найдена");
            }
            return BadRequest("Некорректные данные");
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {            
            return Ok(UserRepository.GetUsers());
        }

        [HttpDelete]
        [Route("id={id}")]        
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (UserManager.FindByIdAsync(id) == null)
            {
                return NotFound("Пользователь не найден");
            }
            else
            {     
                var user = await UserManager.FindByIdAsync(id);
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded && user == null)
                {
                    return Ok("Пользователь удален");
                }
                return BadRequest();
            }
        }

        [HttpGet] 
        [Route("RoleName={name}")]
        public async Task<IActionResult> GetRolesByName(string name)
        {
            if (await RoleManager.FindByNameAsync(name) == null)
            {
                return NotFound("Роль не найдена");
            } 
            var role = await RoleManager.FindByNameAsync(name);
            return Ok(role);            
        }
    }
}
