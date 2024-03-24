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

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    try
        //    {
        //        if (httpContextAccessor.HttpContext == null)
        //            return Unauthorized();

        //        string identityId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        string email = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

        //        var basicInfo = await profileService.Get(identityId);


        //        if(basicInfo != null)
        //        {
        //            return new OkObjectResult(new
        //            {
        //                id = identityId,
        //                firstname = basicInfo.firstname,
        //                lastname = basicInfo.lastname,
        //                email = email,
        //                bio = basicInfo.bio
        //            });
        //        }

        //        return new NotFoundObjectResult(null);

        //    }
        //    catch
        //    {
        //        return BadRequest("Internal server error");
        //    }
        //}


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
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
