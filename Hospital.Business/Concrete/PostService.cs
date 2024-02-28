using Hospital.Business.Abstract;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IAdminService _userService;

        public PostService(IPostDal postDal, IAdminService userService)
        {
            _postDal = postDal;

            _userService = userService;
        }

        /// <summary>
        /// Adds a new post asynchronously.
        /// </summary>
        /// <param name="post">The Post object representing the post to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddPostAsync(Post post)
        {
            await _postDal.AddAsync(post);
        }

        /// <summary>
        /// Retrieves all posts asynchronously.
        /// </summary>
        /// <returns>A collection of Post objects representing all posts.</returns>
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = (await _postDal.GetListAsync()).ToList();

            // Load the user information for each post asynchronously
            await Task.WhenAll(posts.Select(async p => p.Admin = await _userService.GetAdminByIdAsync(p.AdminId)));

            return posts;
        }

        /// <summary>
        /// Retrieves a post by its ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The Post object representing the post with the specified ID.</returns>
        public Task<Post?> GetPostByIdAsync(string postId)
        {
            return _postDal.GetAsync(p => p.Id.ToString() == postId);
        }

        /// <summary>
        /// Retrieves the owner of a post by its ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve the owner for.</param>
        /// <returns>The User object representing the owner of the post.</returns>
        public async Task<Admin?> GetOwnerOfPostByIdAsync(string postId)
        {
            var post = await _postDal.GetAsync(p => p.Id.ToString() == postId);

            var user = await _userService.GetAdminByIdAsync(post.AdminId);

            return user;
        }

        /// <summary>
        /// Deletes all posts of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteUserPostsAsync(string userId)
        {
            var posts = await _postDal.GetListAsync(p => p.AdminId == userId);

            if (posts != null)
            {
                foreach (var post in posts)
                {
                    await _postDal.DeleteAsync(post);
                }
            }
        }

    }
}
