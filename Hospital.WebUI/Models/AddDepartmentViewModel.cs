namespace Hospital.WebUI.Models
{
    public class AddDepartmentViewModel
    {
        public IFormFile? File { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
