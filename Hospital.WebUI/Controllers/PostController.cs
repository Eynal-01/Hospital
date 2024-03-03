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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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


		public async Task<object> CurrentUser()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var role = await _userManager.GetRolesAsync(user);
			dynamic us;
			if (role[0].ToLower() == "patient")
			{
				us = await _context.Patients.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}
			else if (role[0].ToLower() == "admin")
			{
				us = await _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}
			else
			{
				us = await _context.Doctors.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}

			return us;
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
			//for (int i = 0; i < departments.Count(); i++)
			//{
			//    if(view)
			//}
			//var user = await CurrentUser();

			var user = await _userManager.GetUserAsync(HttpContext.User);
			var role = await _userManager.GetRolesAsync(user);
			dynamic us;
			if (role[0].ToLower() == "patient")
			{
				us = await _context.Patients.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}
			else if (role[0].ToLower() == "admin")
			{
				us = await _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}
			else
			{
				us = await _context.Doctors.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}

			var post = new Post
			{
				AdminId = us.Id,
				Title = viewModel.BlogTitle,
				Content = viewModel.Content,
				PublishTime = DateTime.Now.ToShortDateString(),
				DepartmentId = viewModel.DepartmentId
			};

			//foreach (var item in viewModel.Files)
			//{
			for (int i = 0; i < viewModel.Files.Count(); i++)
			{
				if (viewModel.Files[i] != null)
				{
					var helper = new ImageHelper(_webHost);

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
						post.ImageUrl += $"{mediaUrl} - ";
					}
					else
					{
						post.ImageUrl += $"{mediaUrl}";
					}
				}
			}
			//}

			await _context.Posts.AddAsync(post);
			await _context.SaveChangesAsync();

			return RedirectToAction("NewPost", "Post");
		}

		public async Task<PostsShowViewModel> Blog(int postId)
		{
			var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
			var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == post.AdminId);
			var images = post.ImageUrl.Split('-').ToList();
			if (post.ImageUrl != null)
			{
				post.IsImage = true;
			}
			else
			{
				post.IsImage = false;
			}

			for (int k = 0; k < images.Count(); k++)
			{
				images[k] = images[k].TrimStart();
			}

			//var d = await _context.Departments.FirstOrDefaultAsync(d => d.Id == post.DepartmentId);
			//var depart = new Department
			//{
			//    Id = d.Id,
			//    DepartmentName = d.DepartmentName
			//};
			var poo = new PostsShowViewModel();
			poo.PostId = post.Id;
			//poo.Admin = admin;
			poo.AdminId = admin.Id;
			poo.Content = post.Content;
			poo.Title = post.Title;
			poo.PublishTime = post.PublishTime;
			poo.ViewCount = post.ViewCount;
			poo.Images = images;
			poo.DepartmentId = post.DepartmentId;
			//poo.Department = post.DepartmentId;
			return poo;
		}

		public async Task<IActionResult> BlogSingle(int postId)
		{
			var post = await Blog(postId);

			return RedirectToAction("BlogSingle", "Home", post);
		}

		public async Task<IActionResult> BlogSingleAdmin(int postId)
		{
			var post = await Blog(postId);

			return RedirectToAction("BlogSingle", "Admin", post);
		}

		public async Task<IActionResult> PostSingleExit(string departmentId)
		{
			var posts = await PostFilter(departmentId);

			return RedirectToAction("BlogSindebar", "Home", posts);
		}

		public async Task<IActionResult> PostFilter(string departmentId)
		{
			string postDepartment = departmentId;
			if (departmentId == "All")
			{
				postDepartment = "";
			}

			var posts = await GetAllPost(postDepartment);
			return Ok(posts);
		}

		public async Task<IActionResult> PopularPosts()
		{
			var popularPostsId = new List<int>();
			var posts = await GetAllPost();
			var popularPosts = await _context.Posts.OrderBy(d => d.ViewCount).Take(3).ToListAsync();

			foreach (var post in popularPosts)
			{
				popularPostsId.Add(post.Id);
			}

			return Ok(new { posts = posts, popularPostsId = popularPostsId });
		}


		public async Task<IActionResult> GetAllPost(string departmentId = "")
		{
			//var user = await CurrentUser();
			//var d = user.GetType();

			var user = await _userManager.GetUserAsync(HttpContext.User);
			var role = await _userManager.GetRolesAsync(user);
			//dynamic user;
			var admin = new Admin();
			var patient = new Patient();
			var doctor = new Doctor();

			if (role[0].ToLower() == "patient")
			{
				patient = await _context.Patients.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}
			else if (role[0].ToLower() == "admin")
			{
				admin = await _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}
			else
			{
				doctor = await _context.Doctors.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
			}

			var data = await _context.Admins.ToListAsync();
			var posts = new List<PostsShowViewModel>();

			var departments = await _context.Departments.ToListAsync();
			if (departments.Count() == 0)
			{
				var newDeparetmentId = "1";

				var department = new Department
				{
					Id = newDeparetmentId,
					DepartmentName = "All Departmets",
				};

				await _context.Departments.AddAsync(department);
				await _context.SaveChangesAsync();
			}


			foreach (var item in data)
			{
				List<Post> post;
				if (departmentId != string.Empty)
				{
					post = await _context.Posts.Include(nameof(Post.Admin)).Include(nameof(Post.Patient)).Include(nameof(Post.Doctor)).Where(p => p.AdminId == item.Id && p.DepartmentId == departmentId).ToListAsync();
				}
				else
				{
					post = await _context.Posts.Where(p => p.AdminId == item.Id).ToListAsync();
				}
				for (int i = 0; i < post.Count(); i++)
				{
					var images = post[i].ImageUrl.Split('-').ToList();

					var postView = new PostView();
					var poo = new PostsShowViewModel();

					if (role[0].ToLower() == "admin")
					{
						postView = await _context.PostViews.FirstOrDefaultAsync(f => f.AdminId == admin.Id && f.PostId == post[i].Id);
					}
					else if (role[0].ToLower() == "doctor")
					{
						postView = await _context.PostViews.FirstOrDefaultAsync(f => f.DoctorId == doctor.Id && f.PostId == post[i].Id);
					}
					else if (role[0].ToLower() == "patient")
					{
						postView = await _context.PostViews.FirstOrDefaultAsync(f => f.PatientId == patient.Id && f.PostId == post[i].Id);
					}

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


					if (postView != null)
					{
						poo.IsPostView = true;
					}
					else
					{
						poo.IsPostView = false;
					}

					poo.PostId = post[i].Id;
					poo.Admin = item;
					poo.Content = post[i].Content;
					poo.Title = post[i].Title;
					poo.PublishTime = post[i].PublishTime;
					poo.ViewCount = post[i].ViewCount;
					poo.Images = images;
					poo.Department = post[i].Department;

					//var poo = await Blog(post[i].Id);


					posts.Add(poo);
				}
			}
			return Ok(new { posts = posts });
		}
	}
}
