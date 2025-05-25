using BTLweb.Models;
using BTLweb.Services;
using BTLweb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BTLweb.Services
{
    public interface ILoginService
    {
        Task<Users?> GetUserByEmailAsync(string email);
        Task<bool> LoginAsync(LoginViewModel model, HttpContext httpContext);
    }
}
