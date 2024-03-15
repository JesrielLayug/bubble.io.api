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
        public async Task Add(DTOProfileBasicInfo request)
        {
            try
            {
                if(httpContextAccessor.HttpContext != null)
                {
                    var newProfile = new Profile
                    {
                        Fistname = request.firstname,
                        Lastname = request.lastname,
                        Bio = request.bio,
                        IdentityId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    };

                    await profileRepository.Add(newProfile);
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
