﻿using Hospital.Core.Abstract;
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
    public class Admin : IdentityUser, IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Avatar { get; set; } = "userWithoutPicture.png";
        public string? Country { get; set; }
<<<<<<< HEAD
        public int WorkDaysCount { get; set; }
=======

        public virtual ICollection<Post>? Posts { get; set; }
>>>>>>> bf12e164ee3a684225be48c6f4225e079ee9b71e
    }
}
