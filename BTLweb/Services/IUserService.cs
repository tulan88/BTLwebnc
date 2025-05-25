using BTLweb.Models;
using BTLweb.ViewModels;

namespace BTLweb.Services
{
    public interface IUserService
    {
        UserVM GetUserManagementVM(int currentUserRole);
        IEnumerable<Users> SearchUsers(string searchText);
        IEnumerable<Users> FilterUsers(string searchText, string role, string state);
        bool AddUser(Users user);
        bool UpdateUser(Users user);
        bool DeleteUser(int userId);
    }
}
