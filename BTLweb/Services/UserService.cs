using BTLweb.Models;
using BTLweb.Repositories;
using BTLweb.ViewModels;

namespace BTLweb.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserVM GetUserManagementVM(int currentUserRole)
        {
            return new UserVM
            {
                Users = _userRepository.GetAll(),
                CurrentUserRole = currentUserRole
            };
        }

        public IEnumerable<Users> SearchUsers(string searchText)
        {
            return _userRepository.Search(searchText);
        }

        public IEnumerable<Users> FilterUsers(string searchText, string role, string state)
        {
            return _userRepository.Filter(searchText, role, state);
        }

        public bool AddUser(Users user)
        {
            return _userRepository.Add(user);
        }

        public bool UpdateUser(Users user)
        {
            return _userRepository.Update(user);
        }

        public bool DeleteUser(int userId)
        {
            return _userRepository.Delete(userId);
        }
    }
}
