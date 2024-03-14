using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AddDepartmentViewModel
    {
        [Required]
        public IFormFile? File { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
