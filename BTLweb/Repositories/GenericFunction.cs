using BTLweb.Data;
using BTLweb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BTLweb.Repositories
{
    public class GenericFunction<T> : IGenericFunction<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<GenericFunction<T>> _logger;
        public GenericFunction(ApplicationDbContext context,
                                ILogger<GenericFunction<T>> logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        //Hàm hiển thị tất cả các bản ghi của một bảng
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var result = await _dbSet.ToListAsync();
                _logger.LogInformation($"Đã truy xuất {result.Count} bản ghi từ bảng {typeof(T).Name}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi truy xuất tất cả bản ghi từ bảng {typeof(T).Name}");
                throw;
            }
        }

        //Hàm hiển thị tổng hợp bản ghi của nhiều bảng
        //Cách sd: var orders = await GetWithIncludeAsync(o => o.Users, o => o.Articles);
        public async Task<IEnumerable<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy danh sách bản ghi có bao gồm liên kết từ bảng {typeof(T).Name}");
                throw;
            }
        }

        //Lấy bản ghi theo ID
        public async Task<T> GetByIdAsync(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy bản ghi theo ID từ bảng {typeof(T).Name}");
                throw;
            }
        }

        //Thêm bản ghi vào database
        public async Task AddAsync(T ob)
        {
            try
            {
                await _dbSet.AddAsync(ob);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Đã thêm vào bảng {typeof(T).Name} thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Thêm vào bảng {typeof(T).Name} không thành công");
                throw;
            }
        }

        //Update bản ghi
        public async Task UpdateAsync(T ob)
        {
            try
            {
                _dbSet.Update(ob);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Cập nhật bảng {typeof(T).Name} thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cập nhật bảng {typeof(T).Name} không thành công");
                throw;
            }
        }

        //Xóa bản ghi
        public async Task DeleteAsync(T ob)
        {
            try
            {
                _dbSet.Remove(ob);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Xóa bản ghi từ {typeof(T).Name} thành công");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Xóa bản ghi từ {typeof(T).Name} Không thành công");
                throw;
            }
        }

        //Đếm số lượng bản ghi
        //Cách sd: int totalUsers = await _repository.CountAsync();
        //         int activeUsers = await _userRepository.CountAsync(u => u.IsActive);
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return await _dbSet.CountAsync();
                else
                    return await _dbSet.CountAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi đếm số lượng bản ghi trong bảng {typeof(T).Name} Không thành công");
                throw;
            }
        }

        //Kiểm tra tồn tại theo điều kiện
        //Cách sd: bool exists = await _userRepository.ExistsAsync(u => u.Email == "abc@example.com");
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi kiểm tra sự tồn tại trong bảng {typeof(T).Name}");
                throw;
            }
        }

        //Tìm bản ghi theo điều kiện (predicate)
        //Cách sd: var usersOver25 = await _repository.FindAsync(user => user.Age > 25);
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi tìm bản ghi trong bảng {typeof(T).Name}");
                throw;
            }
        }

        //Lấy bản ghi đầu tiên theo điều kiện
        //Cách sd: var user = await userRepository.FirstOrDefaultAsync(u => u.Username == "tulan");
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi không tìm thấy bản ghi theo yêu cầu trong bảng {typeof(T).Name}");
                throw;
            }
        }
    }
}
