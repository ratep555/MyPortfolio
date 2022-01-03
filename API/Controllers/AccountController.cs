using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IAnnualReviewService _annualReviewService1;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly IGoogleAuthService _googleAuthService;


        public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IUserService userService,
        IAnnualReviewService annualReviewService,
        ITokenService tokenService,
        IEmailService emailService, 
        IConfiguration config,
        IGoogleAuthService googleAuthService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _annualReviewService1 = annualReviewService;
            _tokenService = tokenService;
             _emailService = emailService;
            _config = config;
            _googleAuthService = googleAuthService;
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

            if (!await _userManager.IsEmailConfirmedAsync(user)) 
                return Unauthorized(new ServerResponse(401, "Email is not confirmed"));

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

        [HttpPost("externallogin")]
		public async Task<ActionResult<UserDto>> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
		{
			var payload =  await _googleAuthService.VerifyGoogleToken(externalAuth);
			if(payload == null)
				return BadRequest("Invalid External Authentication.");

			var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

			var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(payload.Email);

				if (user == null)
				{
					user = new AppUser 
                    { 
                        Email = payload.Email, 
                        UserName = payload.Email, 
                        DisplayName = payload.Email,
                        EmailConfirmed = true 
                    };

					await _userManager.CreateAsync(user);

					await _userManager.AddLoginAsync(user, info);
				}
				else
				{
					await _userManager.AddLoginAsync(user, info);
				}
			}

			if (user == null)
				return BadRequest("Invalid External Authentication.");

			var token = _tokenService.CreateToken(user);

            return new UserDto
            {
                Email = user.Email,
                Token = token,
                DisplayName = user.Email
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

            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = 
                    $"{_config["ApiAppUrl"]}/api/account/confirmemail?email={user.Email}&token={validEmailToken}";

            await _emailService.SendEmailAsync(user.Email, 
                "Confirm your email", $"<h1>Welcome to MyPortfolio</h1>" +
                $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

            return Ok();
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                return NotFound(new ServerResponse(404));

            await _emailService.ConfirmEmailAsync(email, token);

            return Redirect($"{_config["AngularAppUrl"]}/account/email-confirmation");
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {         
            if (string.IsNullOrEmpty(dto.Email)) return NotFound(new ServerResponse(404));

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return BadRequest(new ServerResponse(400));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_config["AngularAppUrl"]}/account/reset-password?email={dto.Email}&token={validToken}";

            await _emailService.SendEmailAsync(dto.Email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{url}'>Click here</a></p>");   

            return Ok();
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return NotFound(new ServerResponse(404));

            var decodedToken = WebEncoders.Base64UrlDecode(dto.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, dto.Password);

            if (result.Succeeded) return Ok();

            return BadRequest(new ServerResponse(400));
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
        public async Task<ActionResult> LockUser(string id)
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









