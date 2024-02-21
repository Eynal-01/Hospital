using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public int LikeCount { get; set; }
        public DateTime WriteTime { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
        public string? CustomIdentityUserId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual CustomIdentityUser? User { get; set; }
    }
}
