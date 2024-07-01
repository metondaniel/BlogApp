using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.Models;
using BlogApp.Domain;
using BlogApp.Domain.Interfaces.Services;
using BlogApp.Domain.Models;

namespace BlogApp.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly IPostService _postService;

        public IndexModel(IPostService postService)
        {
            _postService = postService;
        }

        public List<Post> Posts { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _postService.GetAllPostsAsync();
        }
    }
}
