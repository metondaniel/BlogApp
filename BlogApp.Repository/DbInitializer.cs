using BlogApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Repository
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(BlogContext context, UserManager<User> userManager)
        {
            context.Database.Migrate();

            if (context.Users.Any())
            {
                return;   
            }
            var users = new User[]
            {
                new User { UserName = "user1@example.com", Email = "user1@example.com" },
                new User { UserName = "user2@example.com", Email = "user2@example.com" },
                new User { UserName = "user3@example.com", Email = "user3@example.com" },
                new User { UserName = "user4@example.com", Email = "user4@example.com" },
                new User { UserName = "user5@example.com", Email = "user5@example.com" }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Password123!");
            }

            var posts = new Post[]
            {
                new Post { Title = "Post 1", Content = "Historias engraçadas", CreatedAt = DateTime.UtcNow, User = users[0] },
                new Post { Title = "Post 2", Content = "Historias de ação", CreatedAt = DateTime.UtcNow, User = users[1] },
                new Post { Title = "Post 3", Content = "Historias novas", CreatedAt = DateTime.UtcNow, User = users[2] },
                new Post { Title = "Post 4", Content = "Historias antigas", CreatedAt = DateTime.UtcNow, User = users[3] },
                new Post { Title = "Post 5", Content = "Ferramentas de negócios", CreatedAt = DateTime.UtcNow, User = users[4] },
                new Post { Title = "Post 6", Content = "Animais valiosos", CreatedAt = DateTime.UtcNow, User = users[0] },
                new Post { Title = "Post 7", Content = "Testes", CreatedAt = DateTime.UtcNow, User = users[1] }
            };

            foreach (var post in posts)
            {
                context.Posts.Add(post);
            }

            context.SaveChanges();
        }
    }
}
