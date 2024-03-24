using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IProfileService
    {
        //Task<DTORequestData?> Get(string identityId);
        Task Add(DTOProfileRequest profile, string userId);
    }
}
