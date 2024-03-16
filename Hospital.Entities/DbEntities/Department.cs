using Hospital.Core.Abstract;
using Hospital.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class Department : IEntity
    {
        public string? Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Content { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
