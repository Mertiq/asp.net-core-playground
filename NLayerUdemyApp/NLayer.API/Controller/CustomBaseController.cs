using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.CustomResponseDto;

namespace NLayer.API.Controller;

[Route("api/[controller]")]
[ApiController]
public abstract class CustomBaseController : ControllerBase
{
    [NonAction]
    protected static IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
    {
        if (response.StatusCode == 204)
            return new ObjectResult(null)
            {
                StatusCode = response.StatusCode
            };

        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}