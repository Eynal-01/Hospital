using Hospital.Core.DataAccess.EntityFramework;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using HospitalProject.Entities.DbEntities;

namespace Hospital.DataAccess.Concrete.EntityFramework
{
    public class EFDoctorDal: EFEntityFrameworkRepositoryBase<Doctor, CustomIdentityDbContext>, IDoctorDal
    {
    }
}
