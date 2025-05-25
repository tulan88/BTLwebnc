using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTLweb.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }

        [ForeignKey("Articles")]
        public int ArticleID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        public string Content { get; set; }

        public DateTime CommentedTime { get; set; } = DateTime.Now;

        public Articles Article { get; set; }
        public Users User { get; set; }
    }
}
