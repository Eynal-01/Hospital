using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class About
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public string? ImageUrl { get; set; }
        public string? BigTitle { get; set; }
        public string? FirstContent { get; set; }
    }
}
