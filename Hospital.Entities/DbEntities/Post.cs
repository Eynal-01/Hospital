using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class Post
    {
        public int Id { get; set; }
        public string? CustomIdentityUserId { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsImage { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public DateTime PublishTime { get; set; }
        public int? LikeCount { get; set; } = 0;
        public int? CommentCount { get; set; } = 0;
        public int DepartmentId { get; set; }

        public CustomIdentityUser? User { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<UserLikedPost>? UserLikedPosts { get; set; }
    }
}
