using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GoodDeeds.API.Controllers;
using GoodDeeds.Core.Dtos;
using GoodDeeds.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoodDeeds.Tests;

public class FamilyMemberControllerTests
{
    private readonly FamilyMemberController _sut;
    private readonly Mock<IFamilyMemberService> _familyMemberServiceMock;

    public FamilyMemberControllerTests()
    {
        var loggerMock = new Mock<ILogger<FamilyMemberController>>();

        _familyMemberServiceMock = new Mock<IFamilyMemberService>();
        _sut = new FamilyMemberController(_familyMemberServiceMock.Object, loggerMock.Object);
    }
    
    [Fact]
    public void FamilyMemberController_Should_GetCreated()
    {
        _sut.Should().NotBeNull();
    }

    [Fact]
    public async Task RetrieveFamilyMemberByIdAsync_Should_Be200Ok_When_RetrieveFamilyMemberByIdAsyncReturnsFamilyDto()
    {
        var fixture = new Fixture();
        var familyMemberDto = fixture.Create<FamilyMemberDto>();
        
        _familyMemberServiceMock.Setup(item => item.RetrieveFamilyMemberByIdAsync(It.IsAny<string>())).ReturnsAsync(familyMemberDto);

        var result = await _sut.RetrieveFamilyMemberByIdAsync(It.IsAny<string>());
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task RetrieveFamilyMemberByIdAsync_Should_Be400BadRequest_When_RetrieveFamilyMemberByIdAsyncReturnsNull()
    {
        FamilyMemberDto? familyMemberDto = null;
        
        _familyMemberServiceMock.Setup(item => item.RetrieveFamilyMemberByIdAsync(It.IsAny<string>())).ReturnsAsync(familyMemberDto);

        var result = await _sut.RetrieveFamilyMemberByIdAsync(It.IsAny<string>());
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(400);
    }
    
    [Fact]
    public async Task RetrieveFamilyMemberByIdAsync_Should_Be500ServerError_When_RetrieveFamilyMemberByIdAsyncThrowsException()
    {
        FamilyMemberDto? familyMemberDto = null;
        
        _familyMemberServiceMock.Setup(item => item.RetrieveFamilyMemberByIdAsync(It.IsAny<string>())).Throws<Exception>();

        var result = await _sut.RetrieveFamilyMemberByIdAsync(It.IsAny<string>());
        
        var objResult = result as ObjectResult;
        objResult?.StatusCode.Should().Be(500);
    }
    
    [Fact]
    public async Task RetrieveFamilyMembersAsync_Should_Be200Ok_When_RetrieveFamilyMembersAsyncReturnsFamilyDtoList()
    {
        var fixture = new Fixture();
        var familyMemberDtos = fixture.Create<List<FamilyMemberDto>>();
        
        _familyMemberServiceMock.Setup(item => item.RetrieveFamilyMembersAsync()).ReturnsAsync(familyMemberDtos);

        var result = await _sut.RetrieveFamilyMembersAsync();
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(200);
    }
    
    
    [Fact]
    public async Task RetrieveFamilyMembersAsync_Should_Be200Ok_When_RetrieveFamilyMembersAsyncThrowsException()
    {
        _familyMemberServiceMock.Setup(item => item.RetrieveFamilyMembersAsync()).Throws<Exception>();

        var result = await _sut.RetrieveFamilyMembersAsync();
        
        var objResult = result as ObjectResult;
        objResult?.StatusCode.Should().Be(500);
    }
    
    [Fact]
    public async Task CreateFamilyMemberAsync_Should_Be200Ok_When_AddFamilyMemberAsyncReturnsNewFamilyMemberDto()
    {
        var fixture = new Fixture();
        var newFamilyMemberDto = fixture.Create<FamilyMemberDto>();
        
        _familyMemberServiceMock.Setup(item => item.AddFamilyMemberAsync(It.IsAny<FamilyMemberDto>())).ReturnsAsync(newFamilyMemberDto);

        var result = await _sut.CreateFamilyMemberAsync(It.IsAny<FamilyMemberDto>());
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task CreateFamilyMemberAsync_Should_Be400BadRequest_When_AddFamilyMemberAsyncThrowsAutoMapperMappingException()
    {
        _familyMemberServiceMock.Setup(item => item.AddFamilyMemberAsync(It.IsAny<FamilyMemberDto>())).Throws<AutoMapperMappingException>();

        var result = await _sut.CreateFamilyMemberAsync(It.IsAny<FamilyMemberDto>());
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(400);
    }
    
    [Fact]
    public async Task CreateFamilyMemberAsync_Should_Be500ServerError_When_AddFamilyMemberAsyncThrowsException()
    {
        _familyMemberServiceMock.Setup(item => item.AddFamilyMemberAsync(It.IsAny<FamilyMemberDto>())).Throws<Exception>();

        var result = await _sut.CreateFamilyMemberAsync(It.IsAny<FamilyMemberDto>());
        
        var objResult = result as ObjectResult;
        objResult?.StatusCode.Should().Be(500);
    }
    
    [Fact]
    public async Task UpdateFamilyMemberAsync_Should_Be200Ok_When_UpdateFamilyMemberAsyncReturnsNewFamilyMemberDto()
    {
        var fixture = new Fixture();
        var familyMemberDto = fixture.Create<FamilyMemberDto>();
        
        _familyMemberServiceMock.Setup(item => item.UpdateFamilyMemberAsync(It.IsAny<FamilyMemberDto>())).ReturnsAsync(familyMemberDto);

        var result = await _sut.UpdateFamilyMemberAsync(It.IsAny<FamilyMemberDto>());
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task UpdateFamilyMemberAsync_Should_Be400BadRequest_When_UpdateFamilyMemberAsyncThrowsAutoMapperMappingException()
    {
        _familyMemberServiceMock.Setup(item => item.UpdateFamilyMemberAsync(It.IsAny<FamilyMemberDto>())).Throws<AutoMapperMappingException>();

        var result = await _sut.UpdateFamilyMemberAsync(It.IsAny<FamilyMemberDto>());
        
        var objResult = result as ObjectResult;
        objResult.Should().NotBeNull();
        objResult?.StatusCode.Should().Be(400);
    }
    
    [Fact]
    public async Task UpdateFamilyMemberAsync_Should_Be500ServerError_When_UpdateFamilyMemberAsyncThrowsException()
    {
        _familyMemberServiceMock.Setup(item => item.UpdateFamilyMemberAsync(It.IsAny<FamilyMemberDto>())).Throws<Exception>();

        var result = await _sut.UpdateFamilyMemberAsync(It.IsAny<FamilyMemberDto>());
        
        var objResult = result as ObjectResult;
        objResult?.StatusCode.Should().Be(500);
    }
    
    [Fact]
    public void DeleteFamilyMember_Should_Be204NoContent_When_DeleteFamilyMemberSuccessful()
    {
        var result = _sut.DeleteFamilyMember(It.IsAny<string>());
        
        var objResult = result as ObjectResult;
        objResult?.StatusCode.Should().Be(204);
    }
    
    [Fact]
    public void DeleteFamilyMember_Should_Be500InternalServerError_When_DeleteFamilyMemberThrowsException()
    {
        var result = _sut.DeleteFamilyMember(It.IsAny<string>());
        
        _familyMemberServiceMock.Setup(item => item.DeleteFamilyMember(It.IsAny<string>())).Throws<Exception>();
        
        var objResult = result as ObjectResult;
        objResult?.StatusCode.Should().Be(500);
    }
}