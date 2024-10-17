using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Store.Data.Entities.IdentityEntities;
using Store.Service.HandleResponses;
using Store.Service.Services.UserService;
using Store.Service.Services.UserService.Dtos;

namespace Store.Web.Controllers
{
   
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService , UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = _userService.Login(loginDto);

            if (user == null)
            {
                return BadRequest(new CustomException(400, "Email not found"));
            }
            return Ok(user);
        }

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = _userService.Register(registerDto);

            if (user == null)
            {
                return BadRequest(new CustomException(400, "Email is exist"));
            }
            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<UserDto> GetCurrentUserDetails()
        {
            var userId = User?.FindFirst("UserId");

            var user = await _userManager.FindByIdAsync(userId.Value);

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                DisplayName = user.DisplayName,
                Email = user.Email,
            };
        }


    }
}
