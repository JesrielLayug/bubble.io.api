using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IProfileService
    {
        Task<DTOProfileBasicInfo?> Get(string identityId);
        Task Add(DTOProfileBasicInfo request);
    }
}
