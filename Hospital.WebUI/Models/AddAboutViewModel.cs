using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AddAboutViewModel
    {
        //public string? ImageUrl { get; set; }
        //[Required]
        //public IFormFile? File { get; set; }
        //[Required]
        //public string? Title { get; set; }
        public int? AboutsCount { get; set; }
        //[Required]
        //public string? Content { get; set; }
        [Required]
        public string? BigTitle { get; set; }
        [Required]
        public string? FirstContent { get; set; }
        public int Id { get; set; }
    }
}
