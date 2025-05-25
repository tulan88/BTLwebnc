using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTLweb.Models
{
    public class Subscriptions
    {
        [Key]
        public int SubscriptionID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        [ForeignKey("Categories")]
        public int CategoryID { get; set; }

        public DateTime SubscribedTime { get; set; } = DateTime.Now;

        [ForeignKey("UserID")]
        public virtual Users Reader { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Categories Category { get; set; }
    }
}
