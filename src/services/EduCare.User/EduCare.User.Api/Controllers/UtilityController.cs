using System.ComponentModel.DataAnnotations;
using EduCare.User.Api.BackgroundServices.UserUpload;
using EduCare.User.Core.Abstract.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EduCare.User.Api.Controllers;

[ApiController]
[Route("api/users/utility")]
public class UtilityController : ControllerBase
{
    private readonly IServiceScopeFactory _serviceFactory;
    public UtilityController(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    [HttpPost("upload-user-file")]
    [RequestSizeLimit(200_000_000)]
    public async Task<IActionResult> UserUpload(IFormFile file, [FromServices] IUserUploadTaskQueue userUploadServiceQueue)
    {

        if (file.Length <= 0) return BadRequest(new { message = "File not valid" });


        var filePath = Path.GetTempFileName();
        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);

            await userUploadServiceQueue
                .QueueWorkItem((new UserUploadTask(filePath, _serviceFactory).Process));
        }
        return Ok();
    }
}