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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager,IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
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
                return ex.Message;
            }
            return "Error Encountered";
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault((u) => u.UserName.ToLower() == loginRequestDto.Username.ToLower());
            if (user == null)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(!isPasswordValid)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhomrNumber = user.PhoneNumber,
            };
            return new LoginResponseDto() { User = userDto, Token = token  };
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            var user = _db.ApplicationUsers.FirstOrDefault((u) => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }
    }
}
