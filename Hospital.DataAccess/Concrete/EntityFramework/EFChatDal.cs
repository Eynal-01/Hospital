
using Hospital.Core.DataAccess.EntityFramework;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Concrete.EntityFramework
{
    public class EFChatDal: EFEntityFrameworkRepositoryBase<Chat, CustomIdentityDbContext>, IChatDal
    {
    }
}
