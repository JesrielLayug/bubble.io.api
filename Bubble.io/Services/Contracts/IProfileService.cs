using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IProfileService
    {
        Task Add(DTOProfile request);
    }
}
