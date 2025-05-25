using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BTLweb.Models
{
    public class Articles
    {
        [Key]
        public int ArticleID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public string Content { get; set; }

        [StringLength(255)]
        public string ImageURL { get; set; }

        public DateTime PostedTime { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Categories Category { get; set; }

        [ForeignKey("UserID")]
        public virtual Users Author { get; set; }
    }
}
