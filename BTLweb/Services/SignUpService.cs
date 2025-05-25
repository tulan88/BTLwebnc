using BTLweb.Models;
using BTLweb.Repositories;
using BTLweb.ViewModels;
using BTLweb.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BTLweb.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IGenericFunction<Users> _Gr;
        private readonly ISignUpRepository _Sr;
        private readonly IEmailSender _Er;
        private const string GmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public SignUpService(ISignUpRepository Sr, 
                            IGenericFunction<Users> Gr, 
                            IEmailSender Er)
        {
            _Sr = Sr;
            _Gr = Gr;
            _Er = Er;
        }

        // Đăng ký người dùng mới
        public async Task<bool> AddUserAsync(SignUpViewModel model)
        {
            bool exists = await _Gr.ExistsAsync(u => u.Email == model.Email);
            if (exists) return false;

            var confirmCode = Guid.NewGuid().ToString();

            var user = new Users
            {
                FullName = model.FullName,
                Birthday = model.Birthday,
                Email = model.Email, // Lấy thông tin từ model
                Password = HashPassword(model.Password), // Mã hóa mật khẩu
                Role = 0, // Vai trò của người dùng
                State = 0
            };

            // Thêm người dùng vào cơ sở dữ liệu
            await _Gr.AddAsync(user);

            // Gửi mail xác nhận
            string confirmationUrl = $"https://localhost:5001/SignUp/ConfirmEmail?email={model.Email}&code={confirmCode}";
            await _Er.SendEmailAsync(model.Email, "Xác nhận đăng ký",
                $"Vui lòng xác nhận đăng ký bằng cách nhấn vào link: <a href='{confirmationUrl}'>Xác nhận</a>");

            // (Bạn có thể lưu `confirmCode` vào một bảng riêng hoặc đính kèm vào User nếu cần)
            return true;
        }


        // Hàm mã hóa mật khẩu (sử dụng kỹ thuật mã hóa như SHA256, bcrypt...)
        public string HashPassword(string password)
        {
            //phương thức mã hóa
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<bool> ConfirmEmailAsync(string email)
        {
            var user = await _Gr.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.State == 1) return false;

            user.State = 1;
            await _Gr.UpdateAsync(user);
            return true;
        }
    }
}