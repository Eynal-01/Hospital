using Hospital.Business.Abstract;
using Hospital.DataAccess.Abstract;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        public PatientService(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        public async Task AddPatient(Patient patient)
        {
            await _patientDal.AddAsync(patient);
        }

        public async Task DeletePatient(string patientId)
        {
            var patient = await _patientDal.GetAsync(p=>p.Id == patientId); 
            await _patientDal.DeleteAsync(patient);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _patientDal.GetListAsync();
        }

        public async Task<Patient> GetPatientById(string id)
        {
            return await _patientDal.GetAsync(p=>p.Id== id);       
            
        }
    }
}
