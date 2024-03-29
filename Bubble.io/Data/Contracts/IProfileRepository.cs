using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAllExceptCurrentUser(string id);
        Task<Profile> GetByIdentityId(string id);
        Task Add(Profile profile);
        Task Update(Profile profile);
    }
}
