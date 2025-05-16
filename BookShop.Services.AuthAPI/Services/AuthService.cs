using BookShop.Services.AuthAPI.Data;
using BookShop.Services.AuthAPI.Model;
using BookShop.Services.AuthAPI.Model.Dto;
using BookShop.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new()
            {
                Email = registerationRequestDto.Email,
                Name = registerationRequestDto.Name,
                UserName = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email,
                PhoneNumber = registerationRequestDto.PhoneNumber
            };
            try
            {
                var _result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (_result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registerationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhomrNumber = userToReturn.PhoneNumber
                    };
                    return string.Empty;
                }
                else
                {
                    return _result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
            }
            return "Error Encountered";
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
