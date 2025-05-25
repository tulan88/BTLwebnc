using BTLweb.Models;
using System.Threading.Tasks;

namespace BTLweb.Repositories
{
    public interface ISignUpRepository
    {
        Task ConfirmEmailAsync(string email);
    }
}