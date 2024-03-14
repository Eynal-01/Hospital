using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
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

        //[Required]
        public string? Gender { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public int MobileNumber { get; set; }
        public string? ImageUrl { get; set; } = "userWithoutPicture.jpg";
        public IFormFile? File { get; set; }
        [Required]
        public string? ShortBiography { get; set; }
        [Required]
        public string? Education { get; set; }
        public List<Department>? Departments { get; set; }
        public List<Schedule>? Schedules { get; set; }
        public List<Room>? Rooms { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        public string? ScheduleId { get; set; }
        public int? RoomId { get; set; }
    }
}
