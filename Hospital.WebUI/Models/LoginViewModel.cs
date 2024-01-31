using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Selected { get; set; }
    }
}
