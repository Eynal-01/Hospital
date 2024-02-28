using HospitalProject.Entities.DbEntities;
using System.Security.Principal;

namespace Hospital.WebUI.Models
{
    public class NewPostViewModel
    {
        public string? BlogTitle { get; set; }
        public string? ImageUrl { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? DepartmentId { get; set; }
        public List<Department>? Departments { get; set; }
        public string? Content { get; set; }
    }
}
