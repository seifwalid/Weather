using Microsoft.EntityFrameworkCore;
using Weather.Data;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Repository
{
    public class LimitRepository : ILimitRepository
    {
        private readonly DataContext _context;
        public LimitRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<PermissibleLimits> CreateLimitAsync(PermissibleLimits limitModel)
        {
            await _context.PermissibleLimits.AddAsync(limitModel);
            await _context.SaveChangesAsync();
            return limitModel;
        }

        public async Task<PermissibleLimits?> DeleteLimitAsync(int id)
        {
            var existingLimits=await _context.PermissibleLimits.FirstOrDefaultAsync(l=>l.Id==id);
            if (existingLimits==null) {
                return null;
            
            }

            _context.PermissibleLimits.Remove(existingLimits);
            await _context.SaveChangesAsync();
            return existingLimits;
        }

        public async Task<List<PermissibleLimits>> GetAllLimitsAsync()
        {
            return await _context.PermissibleLimits.ToListAsync();
        }

        public async Task<PermissibleLimits?> GetLimitByIdAsync(int id)
        {
            return await _context.PermissibleLimits.SingleOrDefaultAsync(l=>l.Id==id);
        }

        public Task<PermissibleLimits> GetLimitByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PermissibleLimits?> UpdateLimitAsync(int id, PermissibleLimits limitModel)
        {
            var existingLimit = await _context.PermissibleLimits.FindAsync(id);
            if(existingLimit==null)
            {
                return null;
            }

            existingLimit.MaxSulphurDioxide=limitModel.MaxSulphurDioxide;
            existingLimit.MaxTemperature=limitModel.MaxTemperature;
            existingLimit.MaxHumidity=limitModel.MaxHumidity;
            existingLimit.MaxCarbonMonoxide=limitModel.MaxCarbonMonoxide;
            existingLimit.MaxNitrogenDioxide = limitModel.MaxNitrogenDioxide;
            existingLimit.MaxOzone=limitModel.MaxOzone;
            existingLimit.MaxPM2_5 = limitModel.MaxPM2_5;
            existingLimit.MaxPM10 = limitModel.MaxPM10;

            await _context.SaveChangesAsync();
            return existingLimit;

           
        }
    }
}
