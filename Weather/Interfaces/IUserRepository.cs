using Weather.Model;

namespace Weather.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExists(int elderId);
        Task<PermissibleLimits> GetLimitsByUserId(int userId);
        Task<Userr> CreateUserAsync(Userr user);
        Task<Userr?> UpdateUserAsync(int id, Userr userModel);
        Task<Userr?> GetUserByIdAsync(int id);
        Task<Userr?> GetUserByNameandPassAsync(string userName, string pass);


    }
}
