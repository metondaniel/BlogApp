using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlogApp.Models;
using BlogApp.Domain;
using System.Threading.Tasks;
using BlogApp.Domain.Interfaces.Services;
using BlogApp.Domain.Models;

namespace BlogApp.Pages.Posts
{
    public class DeleteModel : PageModel
    {
        private readonly IPostService _postService;

        public DeleteModel(IPostService postService)
        {
            _postService = postService;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _postService.GetPostByIdAsync(id);
            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToPage("Index");
        }
    }
}
