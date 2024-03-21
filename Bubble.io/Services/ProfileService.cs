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
        public async Task AddOrUpdate(Profile profile, string userId, string imageData)
        {
            try
            {
                var existingProfile = await profileRepository.GetByIdentityId(userId);
                profile.IdentityId = userId;

                // Process and save image data
                if (!string.IsNullOrEmpty(imageData))
                {
                    string imageUrl = "./Resources/" + Guid.NewGuid().ToString() + ".png";
                    byte[] imageBytes = Convert.FromBase64String(imageData);

                    await File.WriteAllBytesAsync(imageUrl, imageBytes);

                    profile.ImageUrl = imageUrl;
                }

                if (existingProfile == null)
                {
                    await profileRepository.Add(profile);
                }
                else
                {
                    profile.Id = existingProfile.Id; 
                    await profileRepository.Update(profile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //public async Task<DTORequestData?> Get(string identityId)
        //{
        //    try
        //    {
        //        var domainBasicInfo = await profileRepository.GetByIdentityId(identityId);
        //        if(domainBasicInfo != null)
        //            return new DTORequestData
        //            {
        //                firstname = domainBasicInfo.Firstname,
        //                lastname = domainBasicInfo.Lastname,
        //                bio = domainBasicInfo.Bio,
        //            };

        //        return null;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
