using Bubble.io.Data.Contracts;
using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using Bubble.io.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bubble.io.Services
{
    public class ProfileService : IProfileService
    {
        private ImageDirectoryManager imageDirectoryManager = new ImageDirectoryManager();

        private readonly IProfileRepository profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }
        public async Task Add(DTOProfileRequest profile, string userId)
        {
            // Process and save image data
            if (!string.IsNullOrEmpty(profile.ImageData))
                profile.ImageUrl = await imageDirectoryManager.Add(userId, profile.ImageUrl.Replace('\\', '/'), profile.ImageData);

            var newProfile = new Profile
            {
                Firstname = profile.firstname,
                Lastname = profile.lastname,
                Bio = profile.bio,
                ImageUrl = profile.ImageUrl,
                IdentityId = userId,
            };

            await profileRepository.Add(newProfile);
        }

        public async Task<DTOProfileData?> Get(string identityId, string email)
        {
            var domainBasicInfo = await profileRepository.GetByIdentityId(identityId);

            string base64Image;

            if (domainBasicInfo != null)
            {

                base64Image = await imageDirectoryManager.GetUserImage(domainBasicInfo.ImageUrl);

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
            else
            {
                base64Image = await imageDirectoryManager.GetDefaultImage();

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
        }

        public async Task Update(DTOProfileRequest profile, string userId)
        {
            var existingProfile = await profileRepository.GetByIdentityId(userId);
            if (existingProfile == null)
                throw new Exception("Unauthrorized");

            existingProfile.Firstname = profile.firstname;
            existingProfile.Lastname = profile.lastname;
            existingProfile.Bio = profile.bio;
            existingProfile.ImageUrl = await imageDirectoryManager.Add(userId, profile.ImageUrl, profile.ImageData);

            await profileRepository.Update(existingProfile);
        }

        public async Task<IEnumerable<DTOProfileData>> GetAllExceptCurrentUser(string identityId, string email)
        {
            var domainUsers = await profileRepository.GetAllExceptCurrentUser(identityId);
            var usersExceptCurrentUser = new List<DTOProfileData>();

            foreach (var user in domainUsers)
            {
                string imageData = string.Empty;

                if (imageDirectoryManager.GetUserImage(user.ImageUrl) != null)
                    imageData = await imageDirectoryManager.GetUserImage(user.ImageUrl);
                else
                    imageData = await imageDirectoryManager.GetDefaultImage();

                usersExceptCurrentUser.Add(new DTOProfileData
                {
                    id = user.IdentityId,
                    firstname = user.Firstname,
                    lastname = user.Lastname,
                    bio = user.Bio,
                    email = email,
                    imageUrl = user.ImageUrl,
                    imageData = imageData
                });
            }

            return usersExceptCurrentUser;
        }
    }
}
