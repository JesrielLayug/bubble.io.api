using Bubble.io.Entities.DTOs;

namespace Bubble.io.Services.Contracts
{
    public interface IProfileImageService
    {
        Task Add(DTOProfileImage request);
    }
}
