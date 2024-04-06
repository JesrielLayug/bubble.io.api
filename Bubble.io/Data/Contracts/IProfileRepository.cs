using Bubble.io.Entities;

namespace Bubble.io.Data.Contracts
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAllExceptCurrentUser(string id);
        Task<Profile> GetByIdentityId(string id);
        Task<IEnumerable<Profile>> GetAll();
        Task Add(Profile profile);
        Task Update(Profile profile);
    }
}
