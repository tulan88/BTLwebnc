using BTLweb.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace BTLweb.Repositories
{
    //Cách sd: private readonly IGenericFunction<User> _userRepo;

    //          public UserController(IGenericFunction<User> userRepo)
    //          { _userRepo = userRepo;}

    public interface IGenericFunction<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(object id);
        Task AddAsync(T ob);
        Task UpdateAsync(T ob);
        Task DeleteAsync(T ob);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
