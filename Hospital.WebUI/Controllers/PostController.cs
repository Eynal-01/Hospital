using Hospital.Business.Abstract;
using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    public class PostController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private IWebHostEnvironment _webHost;
        private readonly CustomIdentityDbContext _context;

        public PostController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _context = context;
        }



        public IActionResult Index()
        {
            return View();
        }




        public async Task<IActionResult> GetAllPost()
        {
            //var user = await CurrentUser();

            var data = await _context.Admins.ToListAsync();
            var posts = new List<PostsShowViewModel>();
            foreach (var item in data)
            {
                var post = await _context.Posts.Where(p => p.AdminId == item.Id).ToListAsync();
                for (int i = 0; i < post.Count(); i++)
                {
                    var images = post[i].ImageUrl.Split(':').ToList();
                    if (post[i].ImageUrl != null)
                    {
                        post[i].IsImage = true;
                    }
                    else
                    {
                        post[i].IsImage = false;
                    }

                    for (int k = 0; k < images.Count(); k++)
                    {
                        images[k] = images[k].TrimStart();
                    }

                    var poo = new PostsShowViewModel();
                    poo.PostId = post[i].Id;
                    poo.Admin = item;
                    poo.Content = post[i].Content;
                    poo.Title = post[i].Title;
                    poo.PublishTime = post[i].PublishTime;
                    poo.ViewCount = post[i].ViewCount;
                    poo.Images = images;
                    poo.DepartmentName = post[i].DepartmentName;

                    posts.Add(poo);
                }
            }
            return Ok(new { posts = posts });
        }


    }
}
