using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;

namespace Bubble.io.Extensions
{
    public static class ProfileExtention
    {
        public static IEnumerable<DTOProfile> Convert(
            this IEnumerable<Profile> profiles,
            IEnumerable<ProfileImage> images)
        {
            return
            (
                from profile in profiles
                join image in images on profile.IdentityId equals image.IdentityId
                select new DTOProfile
                {
                    id = profile.IdentityId,
                    fistname = profile.Firstname,
                    lastname = profile.Lastname,
                    bio = profile.Bio,
                    imageUrl = image.ImageUrl,
                    imageData = image.ImageData,
                }
            ).ToList();
        }
    }
}
