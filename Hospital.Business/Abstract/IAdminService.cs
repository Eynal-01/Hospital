using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IAdminService
    {
        Task<Admin?> GetAdminByUsernameAsync(string username);

        Task AddAsync(Admin admin);

        Task<Admin?> GetAdminByIdAsync(string id);

        Task UpdateAsync(Admin admin);

        Task<IEnumerable<Admin>> GetAllAdminsAsync();

        //Task<IEnumerable<Admin>> GetAllUsersOtherThanAsync(string adminId);

        Task DeleteAdminByIdAsync(string adminId);
    }
}
