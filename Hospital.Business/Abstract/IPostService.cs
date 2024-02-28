using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IPostService
    {
        /// <summary>
        /// Adds a new post asynchronously.
        /// </summary>
        /// <param name="post">The post object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddPostAsync(Post post);

        /// <summary>
        /// Retrieves all posts asynchronously.
        /// </summary>
        /// <returns>A collection of Post objects representing all posts.</returns>
        Task<IEnumerable<Post>> GetAllPostsAsync();

        /// <summary>
        /// Retrieves a post by its ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post to be retrieved.</param>
        /// <returns>The Post object representing the post.</returns>
        Task<Post> GetPostByIdAsync(string postId);

        /// <summary>
        /// Retrieves the owner of a post by the post ID asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>The User object representing the owner of the post.</returns>
        Task<Admin> GetOwnerOfPostByIdAsync(string postId);

        /// <summary>
        /// Deletes all posts of a user asynchronously based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserPostsAsync(string userId);
    }
}
