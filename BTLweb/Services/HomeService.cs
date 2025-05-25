using BTLweb.Data;
using BTLweb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTLweb.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Articles>> GetLatestArticlesByCategoryAsync(int categoryId, int count)
        {
            return await _context.Articles
                .Where(a => a.CategoryID == categoryId)
                .OrderByDescending(a => a.PostedTime)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Articles> GetFeaturedArticleAsync()
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Author)
                .OrderByDescending(a => a.PostedTime)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Articles>> GetHighlightArticlesAsync(int count)
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Author)
                .OrderByDescending(a => a.PostedTime)
                .Skip(1)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Articles>> GetLatestArticlesAsync(int count)
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Author)
                .OrderByDescending(a => a.PostedTime)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Categories>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
