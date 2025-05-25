using BTLweb.Data;
using BTLweb.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BTLweb.Repositories
{
    public class SignUpRepository : ISignUpRepository
    {
        private readonly ApplicationDbContext _context;

        public SignUpRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Xác nhận email để lưu thông tin
        public async Task ConfirmEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                // Cập nhật trạng thái xác nhận email
                await _context.SaveChangesAsync();
            }
        }
    }
}