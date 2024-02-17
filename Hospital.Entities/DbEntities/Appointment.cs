using Hospital.Core.Abstract;
using Hospital.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? DoctorId { get; set; }
        public int? DepartmentId { get; set; }
        public string? PatientId { get; set; }
<<<<<<< HEAD
        //public string? AvailableTimeId2 { get; set; }    
        public string? AppointmentDateId { get; set; }
        public string? AppointmentTimeId { get; set; }
=======
>>>>>>> c1fbed677263c2504a18112ba2cf435234fe9d57
        public string? Message { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Department? Department { get; set; }
        public virtual AvailableTime? AvailableTime { get; set; }
        public virtual AvailableDate? AvailableDate { get; set; }
    }
}