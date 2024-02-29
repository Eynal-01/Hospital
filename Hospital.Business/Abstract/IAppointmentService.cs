using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAppointmentsOfPatientById(string patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsOfDoctorById(string doctorId);
        Task AddAppointment(Appointment appointment);
        Task DeleteAppointment(string appointmentId);    


    }
}
