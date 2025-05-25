using BTLweb.Models;

namespace BTLweb.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAll();
        IEnumerable<Users> Search(string searchText);
        IEnumerable<Users> Filter(string searchText, string role, string state);
        bool Add(Users user);
        bool Update(Users user);
        bool Delete(int userId);
        Users GetById(int userId);
    }
}
