using System.ComponentModel.DataAnnotations;

namespace BTLweb.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
