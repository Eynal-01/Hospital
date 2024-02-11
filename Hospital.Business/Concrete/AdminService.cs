using Hospital.Business.Abstract;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly IAdminDal _adminDal;

        public async Task AddAsync(Admin admin)
        {
            await _adminDal.AddAsync(admin);
        }

        public async Task DeleteAdminByIdAsync(string adminId)
        {
            var admin = await _adminDal.GetAsync(a => a.Id == adminId);
            await _adminDal.DeleteAsync(admin);
        }

        public async Task<Admin?> GetAdminByIdAsync(string id)
        {
            return await _adminDal.GetAsync(a => a.Id == id);
        }

        public async Task<Admin?> GetAdminByUsernameAsync(string username)
        {
            return await _adminDal.GetAsync(a => a.UserName == username);
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await _adminDal.GetListAsync();
        }

        public async Task UpdateAsync(Admin admin)
        {
            await _adminDal.UpdateAsync(admin);
        }
    }
}