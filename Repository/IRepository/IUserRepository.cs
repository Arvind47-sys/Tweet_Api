using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;

namespace Tweet_Api.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username);

        Task<bool> EmailExists(string email);

        Task<bool> AddUser(AppUser appUser);

        Task<AppUser> GetUserByUserName(string username);

        Task<AppUser> GetUserByEmail(string email);

        Task<AppUser> AuthenticateUser(LoginDto loginDto);

        Task<bool> Update(AppUser user);

        Task<IEnumerable<AppUser>> GetAllUsers();
    }
}