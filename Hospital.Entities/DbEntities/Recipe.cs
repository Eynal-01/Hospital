using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? RecipeHeader { get; set; }
        public string? Content { get; set; }
        public string? WriteTime { get; set; }
        public string? PatientId { get; set; }
        public string? DoctorId { get; set; }
    }
}
