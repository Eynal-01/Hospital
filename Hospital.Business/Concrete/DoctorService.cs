using Hospital.Business.Abstract;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorDal _doctorDal;

        public DoctorService(IDoctorDal doctorDal)
        {
            _doctorDal = doctorDal;
        }

        public async Task AddDoctor(Doctor doctor)
        {
            await _doctorDal.AddAsync(doctor);
        }

        public async Task DeleteDoctor(string doctorId)
        {
            var doctor = await _doctorDal.GetAsync(d => d.Id == doctorId);
            await _doctorDal.DeleteAsync(doctor);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            var doctors = await _doctorDal.GetListAsync();
            return doctors;
        }

        public async Task<Doctor> GetDoctorById(string id)
        {
            var doctor = await _doctorDal.GetAsync(d => d.Id == id);
            return doctor;
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            await _doctorDal.UpdateAsync(doctor);
        }
    }
}
