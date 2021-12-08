using System.Linq;
using System.Threading.Tasks;
using API.ErrorHandling;
using API.Extensions;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IAnnualReviewService _annualReviewService1;
        private readonly ITokenService _tokenService;

        public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IUserService userService,
        IAnnualReviewService annualReviewService,
        ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _annualReviewService1 = annualReviewService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(User);
            
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                RoleName = await _userService.GetRoleName(user.Id)
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery]string email)
        {             
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ServerResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ServerResponse(401));

            if (user.LockoutEnd != null) return Unauthorized(new ServerResponse(401));
        
            await _annualReviewService1.ActionsRegardingProfitOrLossCardUponLogin(loginDto.Email);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                RoleName = await _userService.GetRoleName(user.Id)
            };            
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult ("Email address is in use");
            }
 
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ServerResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<Pagination<UserToReturnDto>>> GetAllUsers(
            [FromQuery] QueryParameters queryParameters)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var count = await _userService.GetCountForUsers();
            var list = await _userService.GetUsersWithSearchingAndPaging(queryParameters, email);

            return Ok(new Pagination<UserToReturnDto>
                (queryParameters.Page, queryParameters.PageCount, count, list));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("unlock/{id}")]
        public async Task<ActionResult> UnlockUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);

            if (user == null)
            {
                  return NotFound(new ServerResponse(404));
            }

           await _userService.UnlockUser(id);

           return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("lock/{id}")]
        public async Task<ActionResult> LockUser1(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);

            if (user == null)
            {
                  return NotFound(new ServerResponse(404));
            }

           await _userService.LockUser(id);

           return Ok();
        }


    }
}









