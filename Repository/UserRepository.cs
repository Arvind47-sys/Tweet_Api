using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet_Api.Data;
using Tweet_Api.DTOs;
using Tweet_Api.Entities;
using Tweet_Api.Repository.IRepository;

namespace Tweet_Api.Repository
{
    public class UserRepository : IUserRepository
    {
        public DataContext _context { get; }

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(AppUser appUser)
        {
            await _context.Users.AddAsync(appUser);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }

        public async Task<AppUser> GetUserByUserName(string username)
        {
            return await _context.Users
                       .SingleOrDefaultAsync(x => x.Username == username);
        }

        public async Task<AppUser> AuthenticateUser(LoginDto loginDto)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username.ToLower()
            && x.Password == loginDto.Password);
        }

        public async Task<bool> Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _context.Users
                       .SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}