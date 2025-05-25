using BTLweb.Models;
using BTLweb.Data;
using Microsoft.EntityFrameworkCore;

namespace BTLweb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users.ToList();
        }

        public IEnumerable<Users> Search(string searchText)
        {
            return _context.Users.Where(u =>
                u.FullName.Contains(searchText) ||
                u.Email.Contains(searchText)).ToList();
        }

        public IEnumerable<Users> Filter(string searchText, string role, string state)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(u => u.FullName.Contains(searchText) || u.Email.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(u => u.Role == int.Parse(role));
            }

            if (!string.IsNullOrEmpty(state))
            {
                query = query.Where(u => u.State == int.Parse(state));
            }

            return query.ToList();
        }

        public bool Add(Users user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Users user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Users GetById(int userId)
        {
            return _context.Users.Find(userId);
        }
    }
}
