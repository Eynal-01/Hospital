using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class AllAboutsViewModel
    {
        public List<About> Abouts { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
