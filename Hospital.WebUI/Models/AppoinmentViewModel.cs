using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AppoinmentViewModel
    {
        [Required]
        public string? DoctorId { get; set; }

        [Required]
        public string? DepartmentId { get; set; }
        public string? AvailableTimeId { get; set; }
        [Required]
        public string? AppointmentTime { get; set; }
        public string? AvailableDateId { get; set; }
        [Required]
        public DateTime? AppointmentDate { get; set; }
        public List<Doctor>? Doctors { get; set; }
        public List<Department>? Departments { get; set; }
        //public DateTime Date { get; set; }
        public DateTime DateInTime { get; set; }
        [Required]
        public string? Fullname { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string? Message { get; set; }
        public List<AvailableTime>? AvailableTimes { get; set; }
        public List<AvailableDate>? AvailableDates { get; set; }
    }
}