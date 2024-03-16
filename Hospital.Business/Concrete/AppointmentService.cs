using Hospital.Business.Abstract;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentDal _appointmentDal;
        public AppointmentService(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public async Task AddAppointment(Appointment appointment)
        {
            await _appointmentDal.AddAsync(appointment);
        }

        public async Task DeleteAppointment(string appointmentId)
        {
            var appointment = await _appointmentDal.GetAsync(a => a.Id.ToString() == appointmentId);
            await _appointmentDal.DeleteAsync(appointment);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsOfDoctorById(string doctorId)
        {
            var appointmentsOfDoctor = await _appointmentDal.GetListAsync(a => a.DoctorId.ToString() == doctorId);
            return appointmentsOfDoctor;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsOfPatientById(string patientId)
        {
            var appointmentsOfPatient = await _appointmentDal.GetListAsync(a => a.PatientId == patientId);
            return appointmentsOfPatient;
        }
    }
}
