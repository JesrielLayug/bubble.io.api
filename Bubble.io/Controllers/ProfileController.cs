using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bubble.io.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IProfileImageService profileImageService;

        public ProfileController(IProfileService profileService, IProfileImageService profileImageService)
        {
            this.profileService = profileService;
            this.profileImageService = profileImageService;
        }


        [HttpPost(Name = "AddProfileInfo")]
        public async Task<IActionResult> AddInfo([FromBody] DTOProfile request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await profileService.Add(request);
                    return Ok("Successfully your profile.");
                }

                return BadRequest("Internal server error");
            }
            catch
            {
                return Unauthorized();
            }
        }


        [HttpPost(Name = "AddProfileImage")]
        public async Task<IActionResult> AddImage([FromBody] DTOProfileImage request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await profileImageService.Add(request);
                    return Ok("Successfully uploaded the image.");
                }
                return BadRequest("Internal server error");
            }
            catch
            {
                return Unauthorized();
            }
        }

    }
}
