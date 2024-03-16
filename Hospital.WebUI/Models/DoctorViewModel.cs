using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class DoctorViewModel
    {
        public List<Doctor>? Doctors { get; set; }
        public List<Department>? Departments { get; set; }
    }
}
