﻿using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class UserLikedPost
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int PostId { get; set; }

        public CustomIdentityUser? User { get; set; }
        public Post? Post { get; set; }
    }
}
