using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AppoinmentViewModel
    {
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public string? AvailableTime { get; set; }
        public DateTime Date { get; set; }
        public string? Fullname { get; set; }
        public int PhoneNumber { get; set; }
        public string? Message { get; set; }
        //public int? AvailableTimeIds { get; set; }
        public List<Doctor>? Doctors { get; set; }
        public List<Department>? Departments { get; set; }
        public List<AvailableTime>? AvailableTimes { get; set; }
    }
}