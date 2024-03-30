using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AddHospitalInfoViewModel
    {
        [Required]
        public string? PhoneNumber { get; set; }
        public string? HospitalWorkingStartTime { get; set; }
        public string? HospitalWorkingEndTime { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public bool IsUpdateInfo { get; set; }
    }
}
