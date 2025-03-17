using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Souq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new { message = "Bad Request - 400", error = "Invalid input or missing parameters." });
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized(new { message = "Unauthorized - 401", error = "You must be authenticated to access this resource." });
        }

        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {
            return Forbid("Forbidden - 403: You don't have permission to access this resource.");
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound(new { message = "Not Found - 404", error = "The requested resource could not be found." });
        }

        [HttpGet("conflict")]
        public IActionResult GetConflict()
        {
            return Conflict(new { message = "Conflict - 409", error = "The request could not be completed due to a conflict." });
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("Internal Server Error - 500: Something went wrong on the server.");
        }

    }
}
