using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Calendar
    {
        public int Id { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
<<<<<<< HEAD
        //public ICollection<DateTime>? DoctorAvailableTimes { get; set; }
=======
>>>>>>> 2a999889b507aa5389929ff9d5c2622f59f4ec94
        public string? Event { get; set; }
    }
}
