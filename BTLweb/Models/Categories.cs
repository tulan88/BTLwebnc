using System.ComponentModel.DataAnnotations;

namespace BTLweb.Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(100)]
        public string Slug { get; set; }

        public virtual ICollection<Articles> Article { get; set; }
    }
}
