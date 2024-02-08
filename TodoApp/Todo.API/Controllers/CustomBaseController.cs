using Microsoft.AspNetCore.Mvc;
using Todo.Core;

namespace Todo.API;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
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