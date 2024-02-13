using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using StockApi.DTOs.Account;
using StockApi.Models;
using StockApi.Services;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTServices _jwtService;
        private readonly SignInManager<StockUser> _signInManager;
        private readonly UserManager<StockUser> _userManager;
        public AccountController(JWTServices jwtService,
            SignInManager<StockUser> signInManager,
            UserManager<StockUser> userManager)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null) 
            {
                return Unauthorized("Invalid User Or Password!");
            }
            if (!user.EmailConfirmed) { return Unauthorized("Please Confirm Your Email!"); }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid User Or Password!");
            }
            var res = CreateUserDto(user);
            return  res;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
               return BadRequest($"This Email {model.Email} is used before");
            }
            var user = new StockUser 
            { 
                FirstName = model.FirstName.ToLower(),
                LastName =  model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email =  model.Email.ToLower(),
                EmailConfirmed= true
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if(!result.Succeeded) { return BadRequest(result.Errors); }
            return Ok(new JsonResult(new { title = "Account Created", message = "Your account has been created, please confrim your email address" }));
        }
        #region Private Helper Methods
        private UserDto CreateUserDto(StockUser user) 
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = _jwtService.CreateJWT(user)
            };
        }
        private async Task<bool> CheckEmailExistsAsync(string email) 
        {
            return await _userManager.Users.AnyAsync(x=>x.Email==email.ToLower());
        }
        #endregion

    }
}
