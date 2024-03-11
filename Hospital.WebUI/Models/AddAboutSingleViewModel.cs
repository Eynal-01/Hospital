using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AddAboutSingleViewModel
    {
        public string? ImageUrl { get; set; }
        [Required]
        public IFormFile? File { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}
