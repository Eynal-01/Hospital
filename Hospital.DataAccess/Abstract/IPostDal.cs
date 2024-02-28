using Hospital.Core.DataAccess;
using Hospital.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Abstract
{
    internal interface IPostDal : IEntityRepository<Post>
    {
    }
}
