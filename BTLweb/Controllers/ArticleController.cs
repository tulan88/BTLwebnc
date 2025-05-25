using BTLweb.Models;
using BTLweb.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BTLweb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        
        public IActionResult Index(int id)
        {
            var article = _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            // Lấy bình luận cho bài viết
            ViewBag.Comments = _articleService.GetArticleComments(id) ?? new List<Comments>();

            // Lấy bài viết liên quan
            ViewBag.RelatedArticles = _articleService.GetRelatedArticles(article.CategoryID, article.ArticleID) ?? new List<Articles>();

            return View(article);
        }
        
        [HttpPost]
        public IActionResult AddComment(int ArticleID, string CommentContent)
        {
            if (string.IsNullOrEmpty(CommentContent))
            {
                return RedirectToAction("Index", new { id = ArticleID });
            }
            
            // Kiểm tra người dùng đã đăng nhập chưa
            if (!User.Identity.IsAuthenticated)
            {
                // Có thể chuyển hướng đến trang đăng nhập
                // hoặc hiển thị thông báo yêu cầu đăng nhập
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Article", new { id = ArticleID }) });
            }
            
            // Lấy ID của người dùng hiện tại - Giả sử có phương thức GetCurrentUserId
            int currentUserId = GetCurrentUserId(); // Bạn cần thực hiện phương thức này
            
            var comment = new Comments
            {
                ArticleID = ArticleID,
                UserID = currentUserId,
                Content = CommentContent,
                CommentedTime = DateTime.Now
            };
            
            _articleService.AddComment(comment);
            
            return RedirectToAction("Index", new { id = ArticleID });
        }
        
        // Phương thức lấy ID của người dùng hiện tại
        private int GetCurrentUserId()
        {
            // Đây chỉ là ví dụ, bạn cần thực hiện theo cách riêng của bạn
            // để lấy ID người dùng từ hệ thống xác thực của bạn
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            
            // Trả về một giá trị mặc định hoặc xử lý lỗi
            throw new Exception("Không thể lấy ID người dùng");
        }
    }
}