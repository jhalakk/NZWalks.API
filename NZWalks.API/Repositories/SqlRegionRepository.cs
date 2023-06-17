using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbcontext;
        public SqlRegionRepository(NZWalksDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           return  await _dbcontext.regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
           return await _dbcontext.regions.FirstOrDefaultAsync(x=>x.Id==id);
          
        }
           public async Task<Region?> CreateAsync(Region region)
        {
            await _dbcontext.regions.AddAsync(region);
            await _dbcontext.SaveChangesAsync();    
            return region;
        }
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await _dbcontext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingregion != null)
            {
                return null;
            }
            existingregion.Code= region.Code;
            existingregion.Name = region.Name;
            existingregion.RegionImageUrl = region.RegionImageUrl;

            _dbcontext.SaveChanges();
            return existingregion;
        }
        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingregion = await _dbcontext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingregion != null)
            {
                return null;
            }
            _dbcontext.regions.Remove(existingregion);  
             _dbcontext.SaveChangesAsync();
            return existingregion;
        }
    }
}
