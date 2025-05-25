using BTLweb.Models;
using BTLweb.ViewModels;
using BTLweb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BTLweb.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ISignUpService _Ss;

        public SignUpController(ISignUpService Ss)
        {
            _Ss = Ss;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            bool result = await _Ss.AddUserAsync(model);
            if (!result)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                return View(model);
            }

            ViewBag.Message = "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận.";
            return View("Success");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            bool result = await _Ss.ConfirmEmailAsync(email);
            if (result)
                return RedirectToAction("Index", "Login");
            else
                return Content("Xác nhận không thành công hoặc đã xác nhận.");
        }

    }
}