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
        public string? DepartmentId { get; set; }
        public string? PatientId { get; set; }
        public string? AppointmentDateId { get; set; }
        public string? AppointmentTimeId { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Message { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Department? Department { get; set; }
        public virtual AvailableTime? AvailableTime { get; set; }
        public virtual AvailableDate? AvailableDate { get; set; }
    }
}