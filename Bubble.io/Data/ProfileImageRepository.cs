using Bubble.io.Data.Contracts;
using Bubble.io.Entities;

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
    }
}
