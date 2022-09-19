using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Models;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Error/{statusCode}")]
        public IActionResult ErrorHandler(int statusCode)
        {
            IActionResult result = null;
            switch (statusCode)
            {
                case 404:
                    result = NotFound("Page not found!!!");
                    break;

                case 401:
                    result = Unauthorized("You are not authorized");
                    break;
            }

            return result;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Error")]
        public IActionResult Error()
        {
            var exceptionFeaturePath = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var errorPath = exceptionFeaturePath.Path;
            var errorMessage = exceptionFeaturePath.Error.Message;
            return new ObjectResult(new VehicleErrorModel
            {
                ErrorMessage = errorMessage,
                ErrorPath = errorPath
            });
        }
    }
}
