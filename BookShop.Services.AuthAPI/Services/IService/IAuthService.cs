using BookShop.Services.AuthAPI.Model.Dto;

namespace BookShop.Services.AuthAPI.Services.IService
{
    public interface IAuthService
    {
        public Task<string> Register(RegisterationRequestDto registerationRequestDto);
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        public Task<bool> AssignRole(string email,string role);
    }
}
