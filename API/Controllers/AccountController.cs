using API.BLL.Entities.Identity;
using API.BLL.Interfaces;
using API.DTO;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
   
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signIn;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly IAppSeesion appSeesion;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signIn,ITokenService tokenService,
            IMapper mapper,IAppSeesion appSeesion)
        {
            this.userManager = userManager;
            this.signIn = signIn; 
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.appSeesion = appSeesion;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user is null)
                return Unauthorized(new APIResponse(401));
                var result = await signIn.CheckPasswordSignInAsync(user, login.Email, false);
                if (!result.Succeeded)
                    return Unauthorized(new APIResponse(401));
                return new UserDTO
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = tokenService.CreateToken(user)
                };
        }

        [HttpPost ("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (CheckEmailExistanceAsync(register.Email).Result.Value)
            {
                return new BadRequestObjectResult(new APIValidationErrorResponse
                {
                    Errors = new[]
                    {
                        "email adress alreedy exist"
                    }
                });

            }
            var user= new AppUser
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                UserName=register.Email
            };
            var result = await userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
                return BadRequest(new APIResponse(400));
           return new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = tokenService.CreateToken(user)
            };
        }
        [HttpGet("EmailExistes")]
        public async Task<ActionResult<bool>> CheckEmailExistanceAsync([FromQuery] string email)
          => await userManager.FindByEmailAsync(email) != null;
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurentUser()
        {
            var email = User?.FindFirst(ClaimTypes.Email);
            var user=await userManager.FindByEmailAsync($"{email}");
            return new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,

                Token = appSeesion.AuthorizeToken

            };
        }
    }

}
