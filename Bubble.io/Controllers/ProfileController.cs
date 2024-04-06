using Bubble.io.Entities;
using Bubble.io.Entities.DTOs;
using Bubble.io.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bubble.io.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProfileService profileService;

        public ProfileController(
            IHttpContextAccessor httpContextAccessor,
            IProfileService profileService
            )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                DTOProfileData profle = new DTOProfileData();

                var identityId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

                profle = await profileService.Get(identityId, email);

                return new OkObjectResult(profle);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var identityId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

                var users = await profileService.GetAllExceptCurrentUser(identityId, email);
                return new OkObjectResult(users);
            }
            catch( Exception ex ) 
            {
                return BadRequest(ex.Message );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DTOProfileRequest request)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (httpContextAccessor.HttpContext != null)
                    {
                        var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                        await profileService.Add(request, userId);

                        return Ok();
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DTOProfileRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (httpContextAccessor.HttpContext == null)
                        return Unauthorized();

                    var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    await profileService.Update(request, userId);

                    return Ok();
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex);
            }
        }

    }
}
