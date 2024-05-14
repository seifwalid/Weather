using Weather.Model;

namespace Weather.Interfaces
{
    public interface ILimitRepository
    {
        Task<PermissibleLimits> CreateLimitAsync(PermissibleLimits limitModel);
        Task<List<PermissibleLimits>> GetAllLimitsAsync();
        Task<PermissibleLimits?> UpdateLimitAsync(int id, PermissibleLimits limitModel);
        Task<PermissibleLimits?> DeleteLimitAsync(int id);
        Task<PermissibleLimits?> GetLimitByIdAsync(int id);
        Task<PermissibleLimits> GetLimitByUserId(int userId);


    }
}
