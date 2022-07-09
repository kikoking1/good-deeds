using GoodDeeds.Core;
using Microsoft.AspNetCore.Mvc;

namespace GoodDeeds.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyMemberController : ControllerBase
{
    
    private readonly ILogger<FamilyMemberController> _logger;

    public FamilyMemberController(ILogger<FamilyMemberController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("family-member")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFamilyMember([FromBody] FamilyMemberRequest model)
    {
        return Ok();
    }

}