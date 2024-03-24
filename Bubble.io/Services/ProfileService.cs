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
        public async Task Add(DTOProfileRequest profile, string userId)
        {
            try
            {
                // Process and save image data
                if (!string.IsNullOrEmpty(profile.ImageData))
                {
                    string directoryPath = $"./Resources/{userId}";
                    Directory.CreateDirectory(directoryPath);

                    string imageUrl = Path.Combine(directoryPath, profile.ImageUrl.Replace('\\', '/'));
                    byte[] imageBytes = Convert.FromBase64String(profile.ImageData);

                    await File.WriteAllBytesAsync(imageUrl, imageBytes);

                    profile.ImageUrl = imageUrl.Replace('\\', '/');
                }

                var newProfile = new Profile
                {
                    Firstname = profile.firstname,
                    Lastname = profile.lastname,
                    Bio = profile.bio,
                    ImageUrl = profile.ImageUrl,
                    IdentityId = userId
                };

                await profileRepository.Add(newProfile);
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
