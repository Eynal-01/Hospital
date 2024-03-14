using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.WebUI.Models
{
    public class AppoinmentViewModel
    {
        public string? DoctorId { get; set; }
        public string? DepartmentId { get; set; }
        public string? AppointmentTime { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int DateId { get; set; }
        public List<Department>? Departments { get; set; }
        public int PhoneNumber { get; set; }
        public string? Message { get; set; }
    }
}