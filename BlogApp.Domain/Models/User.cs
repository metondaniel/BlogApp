using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogApp.Domain.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; }
    }
}
