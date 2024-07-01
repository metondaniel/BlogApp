using BlogApp.Domain.Interfaces.Services;
using BlogApp.Domain.Models;
using BlogApp.Models;
using BlogApp.Pages.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public PostController(IPostService postService, IHubContext<NotificationHub> hubContext, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _postService = postService;
            _hubContext = hubContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            if (!User.Identity.IsAuthenticated)
            {
                var firstUser = await _userManager.Users.FirstOrDefaultAsync();
                if (firstUser != null)
                {
                    await _signInManager.SignInAsync(firstUser, isPersistent: false);
                }
            }
            return View(posts);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View("Edit",post);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            post.User = user;
            await _postService.CreatePostAsync(post);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Novo post: {post.Title}");

            return View("Index", await _postService.GetAllPostsAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            
            await _postService.UpdatePostAsync(post);
            return View("Index", await _postService.GetAllPostsAsync());
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await _postService.DeletePostAsync(id);
            return View("Index", await _postService.GetAllPostsAsync());
        }
    }
}