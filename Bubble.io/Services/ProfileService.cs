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

        public async Task<DTOProfileData?> Get(string identityId, string email)
        {
            try
            {
                var domainBasicInfo = await profileRepository.GetByIdentityId(identityId);

                byte[] imageBytes;

                if (File.Exists(domainBasicInfo.ImageUrl))
                    imageBytes = await File.ReadAllBytesAsync(domainBasicInfo.ImageUrl);
                else
                    imageBytes = await File.ReadAllBytesAsync($"./Resources/DefaultUserProfile");

                string base64Image = Convert.ToBase64String(imageBytes);

                if (domainBasicInfo != null)
                {
                    return new DTOProfileData
                    {
                        id = domainBasicInfo.Id,
                        firstname = domainBasicInfo.Firstname,
                        lastname = domainBasicInfo.Lastname,
                        bio = domainBasicInfo.Bio,
                        email = email,
                        imageUrl = domainBasicInfo.ImageUrl,
                        imageData = base64Image
                    };
                }

                return new DTOProfileData
                {
                    id = string.Empty,
                    firstname = string.Empty,
                    lastname = string.Empty,
                    bio = string.Empty,
                    email = email,
                    imageUrl = string.Empty,
                    imageData = base64Image
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
