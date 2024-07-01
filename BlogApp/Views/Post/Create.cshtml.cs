using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlogApp.Models;
using BlogApp.Domain;
using System.Threading.Tasks;
using BlogApp.Domain.Interfaces.Services;
using BlogApp.Domain.Models;

namespace BlogApp.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;

        public CreateModel(IPostService postService)
        {
            _postService = postService;
        }

        [BindProperty]
        public Post Post { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _postService.CreatePostAsync(Post);
            return RedirectToPage("/Posts/Index");
        }
    }
}
