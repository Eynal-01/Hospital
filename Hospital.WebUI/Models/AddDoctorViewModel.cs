using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AddDoctorViewModel
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int MobileNumber { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? File { get; set; }
        public string? ShortBiography { get; set; }

    }
}
