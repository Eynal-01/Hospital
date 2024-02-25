using HospitalProject.Entities.DbEntities;
using System.Security.Principal;

namespace Hospital.WebUI.Models
{
    public class NewPostViewModel
    {
        public string? BlogTitle { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? File { get; set; }
        public string? Content { get; set; }
    }
}
