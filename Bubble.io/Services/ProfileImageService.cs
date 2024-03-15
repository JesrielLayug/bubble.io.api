using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using System.Security.Claims;

namespace Bubble.io.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IProfileImageRepository profileImageRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProfileImageService(IProfileImageRepository profileImageRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.profileImageRepository = profileImageRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task Add(DTOProfileImage request)
        {
            try
            {
                if(httpContextAccessor.HttpContext != null)
                {
                    var newImage = new ProfileImage
                    {
                        ImageUrl = request.imageUrl,
                        ImageData = request.imageData,
                        IdentityId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    };

                    await profileImageRepository.Add(newImage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<DTOProfileImage?> Get(string identityId)
        {
            try
            {
                var domainImage = await profileImageRepository.GetByIdentityId(identityId);
                if(domainImage != null)
                    return new DTOProfileImage
                    {
                        imageUrl = domainImage.ImageUrl,
                        imageData = domainImage.ImageData,
                    };

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
