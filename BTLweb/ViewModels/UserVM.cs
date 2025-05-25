using BTLweb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BTLweb.ViewModels
{
    public class UserVM
    {
        public IEnumerable<Users> Users { get; set; }
        public int CurrentUserRole { get; set; }
    }
}
