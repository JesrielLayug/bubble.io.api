using Bubble.io.Entities.DTOs;
using System.Globalization;

namespace Bubble.io.Services.Contracts
{
    public interface IProfileImageService
    {
        Task<DTOProfileImage?> Get(string identityId);
        Task Add(DTOProfileImage request);
    }
}
