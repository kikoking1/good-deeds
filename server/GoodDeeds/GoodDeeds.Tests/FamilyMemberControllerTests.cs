using AutoFixture;
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
}