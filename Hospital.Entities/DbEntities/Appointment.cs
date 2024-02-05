using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public DateTime? AppointmentDate { get; set; }
<<<<<<< HEAD
        public DateTime? AppointmentTime { get; set; }
=======
        public DateTime AppointmentTime { get; set; }
>>>>>>> a3d3af79db5ec86d43d5266b80f27a3880416117
        public string? Status { get; set; }

        public string? PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
    }
}