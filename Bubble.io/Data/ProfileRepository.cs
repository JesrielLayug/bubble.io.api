using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bubble.io.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext db;

        public ProfileRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task Add(Profile profile)
        {
            await db.Profiles.AddAsync(profile);
            await db.SaveChangesAsync();
        }

        public async Task<Profile> GetByIdentityId(string id)
        {
            return await db.Profiles.FirstOrDefaultAsync(p => p.IdentityId == id);
        }

        public async Task Update(Profile profile)
        {
            db.Profiles.Update(profile);
            await db.SaveChangesAsync();
        }
    }
}
