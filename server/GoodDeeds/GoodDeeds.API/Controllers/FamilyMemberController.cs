using AutoMapper;
using GoodDeeds.API.ResponseModels;
using GoodDeeds.Core.Dtos;
using GoodDeeds.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodDeeds.API.Controllers;

[ApiController]
[Route("api/family-members")]
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
                return BadRequest(
                    new ApiError
                    {
                        Message = "No member exists with that provided id."
                    });
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
    public async Task<IActionResult> RetrieveFamilyMembersAsync()
    {
        try
        {
            var familyMemberDtos = await _familyMemberService.RetrieveFamilyMembersAsync();
            
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
    public async Task<IActionResult> CreateFamilyMemberAsync([FromBody] FamilyMemberDto familyMemberDto)
    {
        try
        {
            var newFamilyMemberDto = await _familyMemberService.AddFamilyMemberAsync(familyMemberDto);

            _logger.LogInformation("Adding Family Member was successful");
            return Ok(newFamilyMemberDto);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(
                ex,
                "FamilyMemberDto request body malformed"
            );
            return BadRequest(
                new ApiError
                {
                    Message = "Request body malformed"
                });
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
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateFamilyMemberAsync([FromBody] FamilyMemberDto familyMemberDto)
    {
        try
        {
            var updatedFamilyMemberDto = await _familyMemberService.UpdateFamilyMemberAsync(familyMemberDto);

            _logger.LogInformation("Updating Family Member was successful");
            return Ok(updatedFamilyMemberDto);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(
                ex,
                "FamilyMemberDto request body malformed"
            );
            return BadRequest(
                new ApiError
                {
                    Message = "Request body malformed"
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Exception while updating family member"
            );
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteFamilyMember(string id)
    {
        try
        {
            _familyMemberService.DeleteFamilyMember(id);

            _logger.LogInformation("Deleting Family Member was successful");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Exception while deleting family member"
            );
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}