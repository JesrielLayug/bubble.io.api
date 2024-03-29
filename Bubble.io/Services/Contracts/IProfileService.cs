using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IProfileService
    {
        Task<DTOProfileData> Get(string identityId, string email);
        Task<IEnumerable<DTOProfileData>> GetAllExceptCurrentUser(string identityId, string email);
        Task Add(DTOProfileRequest profile, string userId);
        Task Update(DTOProfileRequest profile, string userId);
    }
}
