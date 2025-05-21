using BookShop.Services.AuthAPI.Model.Dto;
using BookShop.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected readonly ResponseDto responseDto;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            this.responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errorMsg = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                responseDto.IsSucess = false;
                responseDto.Message = errorMsg;
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]  LoginRequestDto loginRequest)
        {
            var data = await _authService.Login(loginRequest);
            if (data.User == null)
            {
                responseDto.IsSucess = false;
                responseDto.Message = "Email or Password is incorrect";
                return BadRequest(responseDto);
            }
            responseDto.Result = data;
            return Ok(responseDto);
        }
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto model)
        {
            var data = await _authService.AssignRole(model.Email, model.Role);
            if (data == null)
            {
                responseDto.IsSucess = false;
                responseDto.Message = "Cant add role";
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
