using Hospital.Core.DataAccess.EntityFramework;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;

namespace Hospital.DataAccess.Concrete.EntityFramework
{
    public class EFPostDal : EFEntityFrameworkRepositoryBase<Post, CustomIdentityDbContext>, IPostDal
    {
    }
}