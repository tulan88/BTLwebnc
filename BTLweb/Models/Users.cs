using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace BTLweb.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        public int Role { get; set; }
        public int State { get; set; }

        public virtual ICollection<Articles> Article { get; set; }
        public ICollection<Subscriptions> Subscription { get; set; }
        public ICollection<Newsletters> Newsletter { get; set; }
        public ICollection<Comments> Comment { get; set; }
    }
}
