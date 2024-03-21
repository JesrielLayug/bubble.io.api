using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bubble.io.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProfileService(IProfileRepository profileRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.profileRepository = profileRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task Add(DTOProfileBasicInfo request, string userId)
        {
            try
            {
                var existingProfile = await profileRepository.GetByIdentityId(userId);
                string imageUrl = string.Empty;
                var profile = new Profile();

                if (request.imageData != null)
                {
                    imageUrl = "./Resources/" + request.imageData.FileName;
                    using (var fileStream = new FileStream(imageUrl, FileMode.Create))
                    {
                        await request.imageData.CopyToAsync(fileStream);
                    }
                }

                if (existingProfile == null)
                {
                    profile.Fistname = request.firstname;
                    profile.Lastname = request.lastname;
                    profile.Bio = request.bio;
                    profile.ImageUrl = imageUrl;
                    profile.IdentityId = userId;

                    await profileRepository.Add(profile);
                }
                else
                {

                    existingProfile.Fistname = request.firstname;
                    existingProfile.Lastname = request.lastname;
                    existingProfile.Bio = request.bio;
                    existingProfile.ImageUrl = imageUrl;
                    existingProfile.IdentityId = userId;

                    await profileRepository.Update(existingProfile);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<DTOProfileBasicInfo?> Get(string identityId)
        {
            try
            {
                var domainBasicInfo = await profileRepository.GetByIdentityId(identityId);
                if(domainBasicInfo != null)
                    return new DTOProfileBasicInfo
                    {
                        firstname = domainBasicInfo.Fistname,
                        lastname = domainBasicInfo.Lastname,
                        bio = domainBasicInfo.Bio,
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
