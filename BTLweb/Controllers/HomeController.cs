using BTLweb.Models;
using BTLweb.ViewModels;
using BTLweb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace BTLweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _Hs;

        public HomeController(IHomeService Hs)
        {
            _Hs = Hs;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            // Lưu thời gian truy cập vào cookie
            var lastAccessTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Response.Cookies.Append("LastAccessTime", lastAccessTime, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            // Lấy tất cả cookie hiện có
            var allCookies = new Dictionary<string, string>();
            foreach (var cookie in Request.Cookies)
            {
                allCookies.Add(cookie.Key, cookie.Value);
            }
            var featuredArticle = await _Hs.GetFeaturedArticleAsync();
            var highlightArticles = await _Hs.GetHighlightArticlesAsync(2);
            var categories = await _Hs.GetAllCategoriesAsync();

            List<Articles> latestArticles;
            if (categoryId.HasValue)
            {
                latestArticles = await _Hs.GetLatestArticlesByCategoryAsync(categoryId.Value, 6);
            }
            else
            {
                latestArticles = await _Hs.GetLatestArticlesAsync(6);
            }

            var viewModel = new HomeViewModel
            {
                FeaturedArticle = featuredArticle,
                HighlightArticles = highlightArticles,
                LatestArticles = latestArticles,
                Categories = categories,
                SelectedCategoryId = categoryId,
                LastAccessTime = lastAccessTime
            };

            return View(viewModel);
        }
    }
}
