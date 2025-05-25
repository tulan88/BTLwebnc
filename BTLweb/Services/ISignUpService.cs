using BTLweb.Models;
using BTLweb.ViewModels;
using System.Threading.Tasks;

namespace BTLweb.Services
{
    public interface ISignUpService
    {
        Task<bool> AddUserAsync(SignUpViewModel model);
        string HashPassword(string password);
        Task<bool> ConfirmEmailAsync(string email);
    }
}