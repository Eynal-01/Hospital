namespace Hospital.WebUI.Models
{
    public class AddAboutViewModel
    {
        public string ImageUrl { get; set; }
        public IFormFile? File { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
