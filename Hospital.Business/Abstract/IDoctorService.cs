using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IDoctorService
    {
        Task AddDoctor(Doctor Doctor);
        Task DeleteDoctor(string doctorId);
        Task UpdateDoctor(Doctor Doctor);
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor> GetDoctorById(string id);  

    }
}
