using Hospital.Business.Abstract;
using Hospital.DataAccess.Abstract;
using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task AddAsync(CustomIdentityUser user)
        {
            await _userDal.AddAsync(user);
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            var user = _userDal.GetAsync(u=>u.Id== userId);
            //await _userDal.DeleteAsync(user);
        }

        public async Task<IEnumerable<CustomIdentityUser>> GetAllUsersAsync()
        {
            return await _userDal.GetListAsync();
        }

        public Task<IEnumerable<CustomIdentityUser>> GetAllUsersOtherThanAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomIdentityUser?> GetUserByIdAsync(string id)
        {
            return await _userDal.GetAsync(u=>u.Id== id);
        }

        public Task<CustomIdentityUser?> GetUserByUsernameAsync(string username)
        {
            return _userDal.GetAsync(u => u.UserName == username);
        }

        public async Task UpdateAsync(CustomIdentityUser user)
        {
            await _userDal.UpdateAsync(user);
        }

        public Task<bool> UsernameIsTakenAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
