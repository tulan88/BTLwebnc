using BTLweb.Models;

namespace BTLweb.Services
{
    public interface IHomeService
    {
        Task<Articles> GetFeaturedArticleAsync();
        Task<List<Articles>> GetHighlightArticlesAsync(int count);
        Task<List<Articles>> GetLatestArticlesAsync(int count);
        Task<List<Categories>> GetAllCategoriesAsync();
        Task<List<Articles>> GetLatestArticlesByCategoryAsync(int categoryId, int count);

    }
}
