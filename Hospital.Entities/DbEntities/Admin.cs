using Hospital.Core.Abstract;
using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Admin : IdentityUser, IEntity, CommonUsers
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Avatar { get; set; } = "userWithoutPicture.png";
        public string? Country { get; set; }
        public int WorkDaysCount { get; set; }
        public bool? IsPostView { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<PostView>? PostViews { get; set; }
    }
}
