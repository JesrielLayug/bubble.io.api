using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IProfileImageRepository
    {
        Task<ProfileImage> GetByIdentityId(string id);
        Task Add(ProfileImage image);
    }
}
