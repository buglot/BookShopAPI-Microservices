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
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
