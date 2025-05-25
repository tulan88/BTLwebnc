using BTLweb.Models;

namespace BTLweb.ViewModels
{
    public class HomeViewModel
    {
        public Articles FeaturedArticle { get; set; }
        public List<Articles> HighlightArticles { get; set; }
        public List<Articles> LatestArticles { get; set; }
        public List<Categories> Categories { get; set; }
        public int? SelectedCategoryId { get; set; } // dòng này để biết Category nào đang chọn
        public string LastAccessTime { get; set; }
    }
}
