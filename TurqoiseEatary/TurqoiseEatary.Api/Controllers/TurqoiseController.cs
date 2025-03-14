using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace TurqoiseEatary.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class TurqoiseController : ApiController
{
    [HttpGet]
    public IActionResult ListTurqoise()
    {
        return Ok(Array.Empty<string>());

    }
}