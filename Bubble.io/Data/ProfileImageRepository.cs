using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bubble.io.Data
{
    public class ProfileImageRepository : IProfileImageRepository
    {
        private readonly AppDbContext db;

        public ProfileImageRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task Add(ProfileImage image)
        {
            await db.ProfileImages.AddAsync(image);
            await db.SaveChangesAsync();
        }

        public async Task<ProfileImage> GetByIdentityId(string id)
        {
            return await db.ProfileImages.FirstOrDefaultAsync(p => p.IdentityId == id);
        }
    }
}
