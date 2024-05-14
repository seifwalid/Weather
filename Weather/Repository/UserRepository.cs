using Microsoft.EntityFrameworkCore;
using Weather.Data;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Userr> CreateUserAsync(Userr user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<PermissibleLimits> GetLimitsByUserId(int userId)
        {
            var user= await _context.Users.Include(u=>u.UserPermissibleLimits).SingleOrDefaultAsync(u=>u.UserId==userId);
            return user.UserPermissibleLimits;
        }

        public async Task<Userr?> GetUserByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<Userr?> GetUserByNameandPassAsync(string userName, string pass)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName && u.Password == pass);
        }

        public async Task<Userr?> UpdateUserAsync(int id, Userr userModel)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) {
                return null;
            }

            existingUser.Age = userModel.Age;
            existingUser.UserName=userModel.UserName;
            existingUser.Password=userModel.Password;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id);
        }
    }
}
