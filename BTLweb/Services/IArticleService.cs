using BTLweb.Models;

namespace BTLweb.Services
{
    public interface IArticleService
    {
        Articles GetArticleById(int id);
        List<Comments> GetArticleComments(int articleId);
        List<Articles> GetRelatedArticles(int categoryId, int currentArticleId, int count = 3);
        void AddComment(Comments comment);
    }
}
