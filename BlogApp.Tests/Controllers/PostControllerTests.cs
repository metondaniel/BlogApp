using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Controllers;
using BlogApp.Domain.Models;
using BlogApp.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlogApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace BlogApp.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly Mock<IPostService> _mockPostService;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IHubContext<NotificationHub>> _mockPostNotification;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly PostController _controller;

        public PostControllerTests()
        {
            _mockPostService = new Mock<IPostService>();
            _mockPostNotification = new Mock<IHubContext<NotificationHub>>();
            _mockUserManager = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                null, null, null, null, null, null, null, null);

            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                null, null, null, null);

            _controller = new PostController(_mockPostService.Object, _mockPostNotification.Object, _mockSignInManager.Object,_mockUserManager.Object);
        }

        private void SetUserIsAuthenticated(bool isAuthenticated)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "TestUser")
            };

            var identity = new ClaimsIdentity(claims, isAuthenticated ? "TestAuthType" : null);
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

 
        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfPosts()
        {
            var user = new User { UserName = "testuser" };
            var posts = new List<Post>
            {
                new Post { Id = 1, Title = "Post Daniel", Content = "Conteudo daniel" },
                new Post { Id = 2, Title = "Post Test", Content = "Conteudo daniel" }
            };
            _mockPostService.Setup(service => service.GetAllPostsAsync()).ReturnsAsync(posts);

            SetUserIsAuthenticated(true);
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Post>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
