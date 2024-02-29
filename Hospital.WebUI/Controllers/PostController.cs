using CloudinaryDotNet.Actions;
using Hospital.Business.Abstract;
using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using Hospital.WebUI.Abstract;
using Hospital.WebUI.Helpers;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Hospital.WebUI.Controllers
{
    public class PostController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private IWebHostEnvironment _webHost;
        private readonly CustomIdentityDbContext _context;


        private readonly IMediaService _mediaService;


        private readonly IPostService _postService;


        private readonly IAdminService _userService;


        public PostController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context, IMediaService mediaService, IPostService postService, IAdminService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _context = context;
            _mediaService = mediaService;
            _postService = postService;
            _userService = userService;
            //_notificationService = notificationService;
        }



        public IActionResult Index()
        {
            return View();
        }


        public async Task<Admin> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
            return admin;
        }


        [HttpGet]
        public async Task<IActionResult> NewPost()
        {
            var departments = await _context.Departments.ToListAsync();
            var viewModel = new NewPostViewModel
            {
                Departments = departments,
            };
            return RedirectToAction("NewPost", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostViewModel viewModel)
        {
            var user = await CurrentUser();
            var department = new Department();

            if (viewModel.DepartmentId != "All Category")
            {
                department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);
            }
            else
            {
                department.DepartmentName = viewModel.DepartmentId;
            }

            var post = new Post
            {
                AdminId = user.Id,
                Title = viewModel.BlogTitle,
                Content = viewModel.Content,
                PublishTime = DateTime.Now.ToShortDateString(),
                DepartmentName = department.DepartmentName
            };

            //foreach (var item in viewModel.Files)
            //{
            for (int i = 0; i < viewModel.Files.Count(); i++)
            {
                if (viewModel.Files[i] != null)
                {
                    var helper = new ImageHelper(_webHost);
                    var url = await helper.SaveFile(viewModel.Files[i]);

                    var mediaUrl = await _mediaService.UploadMediaAsync(viewModel.Files[i]);

                    if (mediaUrl != string.Empty)
                    {
                        var isVideoFile = _mediaService.IsVideoFile(viewModel.Files[i]);
                    }
                    else
                    {
                        return BadRequest("error");
                    }

                    if (viewModel.Files[i] != viewModel.Files[viewModel.Files.Count() - 1])
                    {
                        post.ImageUrl += $"{url} : ";
                    }
                    else
                    {
                        post.ImageUrl += $"{url}";
                    }
                }
            }
            //}

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("NewPost", "Post");
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
