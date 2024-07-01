using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        [DisplayName("Título")]
        public string Title { get; set; }
        [DisplayName("Conteúdo")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
