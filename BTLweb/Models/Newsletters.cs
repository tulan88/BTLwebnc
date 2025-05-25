using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTLweb.Models
{
    public class Newsletters
    {
        [Key]
        public int NewsletterID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        [ForeignKey("Articles")]
        public int ArticleID { get; set; }

        public DateTime SentTime { get; set; } = DateTime.Now;

        [ForeignKey("UserID")]
        public virtual Users Author { get; set; }
        [ForeignKey("ArticleID")]
        public virtual Articles Article { get; set; }
    }
}
