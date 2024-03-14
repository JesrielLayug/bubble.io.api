using Bubble.io.Data.Contracts;
using Bubble.io.Entities;

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
    }
}
