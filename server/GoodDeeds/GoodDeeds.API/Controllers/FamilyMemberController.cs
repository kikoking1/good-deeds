using GoodDeeds.Core.Dtos;
using GoodDeeds.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodDeeds.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyMemberController : ControllerBase
{
    private readonly IFamilyMemberService _familyMemberService;
    private readonly ILogger<FamilyMemberController> _logger;

    public FamilyMemberController(
        IFamilyMemberService familyMemberService,
        ILogger<FamilyMemberController> logger)
    {
        _familyMemberService = familyMemberService;
        _logger = logger;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RetrieveFamilyMemberByIdAsync(string id)
    {
        try
        {
            var familyMemberDto = await _familyMemberService.RetrieveFamilyMemberByIdAsync(id);

            if (familyMemberDto == null)
            {
                _logger.LogError("Get Family Member was not successful with id: {id}", id);
                return BadRequest();
            }
            
            _logger.LogInformation("Get Family Member was successful with id: {id}", id);
            return Ok(familyMemberDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Exception while retrieving family member with id: {id}", id
            );
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RetrieveFamilyMembers()
    {
        try
        {
            var familyMemberDtos = await _familyMemberService.RetrieveFamilyMembersAsync();

            if (!familyMemberDtos.Any())
            {
                _logger.LogError("Retrieving Family Members was not successful");
                return BadRequest();
            }
            
            _logger.LogInformation("Retrieving Family Members was successful");
            return Ok(familyMemberDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Exception while retrieving family members"
            );
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFamilyMember([FromBody] FamilyMemberDto familyMemberDto)
    {
        try
        {
            await _familyMemberService.AddFamilyMemberAsync(familyMemberDto);

            _logger.LogInformation("Adding Family Member was successful");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Exception while adding family member"
            );
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}