using BTLweb.Models;
using BTLweb.Repositories;
using BTLweb.Services;
using BTLweb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace BTLweb.Services
{
    public class LoginService : ILoginService
    {
        private readonly IGenericFunction<Users> _Gr;
        private readonly ISignUpService _Ss;

        public LoginService(IGenericFunction<Users> Gr,
                            ISignUpService Ss)
        {
            _Gr = Gr;
            _Ss = Ss;
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _Gr.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<bool> LoginAsync(LoginViewModel model, HttpContext httpContext)
        {
            var user = await _Gr.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null && user.State == 1)
            {
                // Sử dụng BCrypt để kiểm tra mật khẩu nhập vào và mật khẩu đã hash trong DB
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                if (isValidPassword)
                {
                    // Xác thực thành công
                    // Tạo danh sách các Claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FullName), // sẽ hiện ở User.Identity.Name
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                        new Claim("UserId", user.UserID.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                    };

                    // Đăng nhập
                    await httpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return true;
                }
            }

            return false;
        }
    }
}
