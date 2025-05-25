using BTLweb.Models;
using BTLweb.Repositories;
using BTLweb.Services;
using BTLweb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BTLweb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _Ls;

        public LoginController(ILoginService Ls)
        {
            _Ls = Ls;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            //if (!ModelState.IsValid) return View(model);

            //bool isValid = await _Ls.LoginAsync(model,HttpContext);
            //if (!isValid)
            //{
            //    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng hoặc chưa xác nhận email.");
            //    return View(model);
            //}

            //// Có thể redirect đến trang chủ nếu đăng nhập thành công
            //return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid) return View(model);
            bool isValid = await _Ls.LoginAsync(model, HttpContext);
            if (!isValid)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng hoặc chưa xác nhận email.");
                return View(model);
            }

            // Đăng nhập thành công => lấy thông tin user từ DB (hoặc tạo thêm trong LoginService nếu thích)
            var userEmail = model.Email;
            var claimsPrincipal = HttpContext.User;

            //// Lấy Role từ Claims
            //var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            //var role = userRoleClaim?.Value;
            var user = await _Ls.GetUserByEmailAsync(userEmail);

            if (user?.Role == 1)
            {
                return RedirectToAction("UserList", "Admin");
            }
            HttpContext.Session.SetInt32("Role", user.Role); // user.Role là giá trị role từ database

            return RedirectToAction("Index", "Home");
        }
    }
}
