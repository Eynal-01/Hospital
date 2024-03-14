using Hospital.Core.DataAccess;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<Admin>
    {
    }
}
