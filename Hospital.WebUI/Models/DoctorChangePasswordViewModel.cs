using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class DoctorChangePasswordViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? NewPassword { get; set; }
        [Required]
        public string? CurrentPassword { get; set; }
        public string? DoctorId { get; set; }
    }
}
