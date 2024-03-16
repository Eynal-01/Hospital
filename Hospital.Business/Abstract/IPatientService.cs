using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IPatientService
    {
        Task AddPatient(Patient Patient);
        Task DeletePatient(string patientId);
        //Task Updateatient(Patient Patient);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientById(string id);
    }
}
