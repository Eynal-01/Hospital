using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IUserService
    {
        Task<CustomIdentityUser?> GetUserByUsernameAsync(string username);

        Task<bool> UsernameIsTakenAsync(string username);

        Task AddAsync(CustomIdentityUser user);

        Task<CustomIdentityUser?> GetUserByIdAsync(string id);

        Task UpdateAsync(CustomIdentityUser user);

        Task<IEnumerable<CustomIdentityUser>> GetAllUsersAsync();

        Task<IEnumerable<CustomIdentityUser>> GetAllUsersOtherThanAsync(string userId);

        Task DeleteUserByIdAsync(string userId);
    }
}
