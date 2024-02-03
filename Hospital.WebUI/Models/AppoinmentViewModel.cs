using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class AppoinmentViewModel
    {
        public List<Doctor>? Doctors { get; set; }
        public List<Department>? Departments { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public int DateInTime { get; set; }
        public string? Fullname { get; set; }
        public int PhoneNumber { get; set; }
        public string? Message { get; set; }
    }
}
