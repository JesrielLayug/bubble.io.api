using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IProfileImageRepository
    {
        Task Add(ProfileImage image);
    }
}
