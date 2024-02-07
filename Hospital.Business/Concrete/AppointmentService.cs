using Hospital.Business.Abstract;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        public Task AddAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAppointment(string appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsOfDoctorById(string doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsOfPatientById(string patientId)
        {
            throw new NotImplementedException();
        }
    }
}
