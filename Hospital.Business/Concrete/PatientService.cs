using Hospital.Business.Abstract;
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
        public Task AddPatient(Patient Patient)
        {
            throw new NotImplementedException();
        }

        public Task DeletePatient(string patientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Patient>> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
