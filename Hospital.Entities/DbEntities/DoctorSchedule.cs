using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class DoctorSchedule
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
        //public ICollection<Department> Departments { get; set; }
        public string? AvailableDays { get; set; }
        //public ICollection<DateTime>? AvailableTimes { get; set; }
        public string? Status { get; set; }
    }
}
