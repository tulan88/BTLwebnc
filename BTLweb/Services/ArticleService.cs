using BTLweb.Data;
using BTLweb.Models;
using BTLweb.Services;
using Microsoft.EntityFrameworkCore;

public class ArticleService : IArticleService
{
    private readonly ApplicationDbContext _context;
    public ArticleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Articles GetArticleById(int id)
    {
        try
        {
            return _context.Articles
                .Include(a => a.Category)
                .Include(a => a.Author)
                .FirstOrDefault(a => a.ArticleID == id);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetArticleById: {ex.Message}");
            return null;
        }
    }

    public List<Comments> GetArticleComments(int articleId)
    {
        try
        {
            return _context.Comments
                .Include(c => c.User)
                .Where(c => c.ArticleID == articleId)
                .OrderByDescending(c => c.CommentedTime)
                .ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetArticleComments: {ex.Message}");
            return new List<Comments>();
        }
    }

    public List<Articles> GetRelatedArticles(int categoryId, int currentArticleId, int count = 3)
    {
        try
        {
            return _context.Articles
                .Where(a => a.CategoryID == categoryId && a.ArticleID != currentArticleId)
                .OrderByDescending(a => a.PostedTime)
                .Take(count)
                .ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in GetRelatedArticles: {ex.Message}");
            return new List<Articles>();
        }
    }

    public void AddComment(Comments comment)
    {
        try
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AddComment: {ex.Message}");
            throw;
        }
    }
}