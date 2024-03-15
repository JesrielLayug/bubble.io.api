using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bubble.io.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProfileImageService profileImageService;

        public ProfileImageController(
            IHttpContextAccessor httpContextAccessor,
            IProfileImageService profileImageService
            )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.profileImageService = profileImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (httpContextAccessor.HttpContext == null)
                    return Unauthorized();

                string currentUserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var image = await profileImageService.Get(currentUserId);

                if(image != null)
                {
                    return new OkObjectResult(new
                    {
                        identityId = currentUserId,
                        url = image.imageUrl,
                        data = image.imageData
                    });
                }
                return new NotFoundObjectResult(null);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DTOProfileImage request)
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
