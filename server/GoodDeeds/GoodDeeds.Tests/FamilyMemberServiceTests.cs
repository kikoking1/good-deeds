using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GoodDeeds.API.MapperProfiles;
using GoodDeeds.Application.Services;
using GoodDeeds.Core.Dtos;
using GoodDeeds.Core.Entities;
using GoodDeeds.Core.Interfaces.Repositories;
using GoodDeeds.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoodDeeds.Tests;

public class FamilyMemberServiceTests
{
    private readonly IFamilyMemberService _sut;
    private readonly Mock<IFamilyMemberRepository> _familyMemberRepository;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IMapper _familyMemberMapper;

    public FamilyMemberServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _familyMemberMapper =
            new Mapper(new MapperConfiguration(config => config.AddProfile<FamilyMemberDtoMapping>()));
        var loggerMock = new Mock<ILogger<FamilyMemberService>>();

        _familyMemberRepository = new Mock<IFamilyMemberRepository>();
        _sut = new FamilyMemberService(_familyMemberRepository.Object, _mapperMock.Object, loggerMock.Object);
    }
    
    [Fact]
    public async Task RetrieveFamilyMemberByIdAsync_Should_Return_FamilyMemberDto_When_Successful()
    {
        var fixture = new Fixture();
        var familyMember = fixture.Create<FamilyMember>();

        _familyMemberRepository
            .Setup(mock => mock.RetrieveByIdAsync(
                It.IsAny<string>()))
            .ReturnsAsync(familyMember);
        
        var familyMemberDto = _familyMemberMapper.Map<FamilyMemberDto>(familyMember);
        
        _mapperMock.Setup(item => item.Map<FamilyMemberDto>(It.IsAny<FamilyMember>()))
            .Returns(familyMemberDto);

        var result = await _sut.RetrieveFamilyMemberByIdAsync(It.IsAny<string>());

        result.Should().NotBe(null);
        result?.Id.Should().Be(familyMember.Id);
        result?.FirstName.Should().Be(familyMember.FirstName);
        result?.LastName.Should().Be(familyMember.LastName);
        result?.GoodDeedPoints.Should().Be(familyMember.GoodDeedPoints);
    }
    
    [Fact]
    public async Task RetrieveFamilyMembersAsync_Should_Return_ListOfFamilyMemberDtos_When_Successful()
    {
        var fixture = new Fixture();
        var familyMembers = fixture.Create<List<FamilyMember>>();

        _familyMemberRepository
            .Setup(mock => mock.RetrieveAllAsync())
            .ReturnsAsync(familyMembers);
        
        var familyMemberDtos = _familyMemberMapper.Map<List<FamilyMemberDto>>(familyMembers);
        
        _mapperMock.Setup(item => item.Map<List<FamilyMemberDto>>(It.IsAny<List<FamilyMember>>()))
            .Returns(familyMemberDtos);

        var result = await _sut.RetrieveFamilyMembersAsync();

        result.Should().NotBeNull();
        for (var x = 0; x < result.Count; x++)
        {
            result[x].Id.Should().Be(familyMemberDtos[x].Id);
            result[x].FirstName.Should().Be(familyMemberDtos[x].FirstName);
            result[x].LastName.Should().Be(familyMemberDtos[x].LastName);
            result[x].GoodDeedPoints.Should().Be(familyMemberDtos[x].GoodDeedPoints);
        }
        
    }
    
    [Fact]
    public async Task RetrieveFamilyMembersAsync_Should_Return_EmptyListOfFamilyMemberDtos_When_RetrieveAllAsyncReturnsEmptyListOfFamilyMember()
    {
        _familyMemberRepository
            .Setup(mock => mock.RetrieveAllAsync())
            .ReturnsAsync(new List<FamilyMember>());

        var result = await _sut.RetrieveFamilyMembersAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<List<FamilyMemberDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddFamilyMemberAsync_Should_Call_AddAsync_When_Successful()
    {
        var fixture = new Fixture();
        var familyMemberDto = fixture.Build<FamilyMemberDto>()
            .Without(x => x.Id)
            .Create();

        var familyMember = _familyMemberMapper.Map<FamilyMember>(familyMemberDto);
        familyMember.Id = fixture.Create<string>();

        _mapperMock.Setup(item => item.Map<FamilyMember>(It.IsAny<FamilyMemberDto>()))
            .Returns(familyMember);
        
        _mapperMock.Setup(item => item.Map(familyMember, familyMemberDto));

        var result = await _sut.AddFamilyMemberAsync(familyMemberDto);

        result.Should().NotBeNull();
        _familyMemberRepository.Verify(mock => mock.AddAsync(It.IsAny<FamilyMember>()), Times.Once);
    }
    
    [Fact]
    public async Task AddFamilyMemberAsync_Should_NotCall_AddAsync_When_MappingFails()
    {
        var fixture = new Fixture();
        var familyMemberDto = fixture.Build<FamilyMemberDto>()
            .Without(x => x.Id)
            .Create();

        _mapperMock.Setup(item => item.Map<FamilyMember>(It.IsAny<FamilyMemberDto>()))
            .Throws<AutoMapperMappingException>();
        
        await Assert.ThrowsAsync<AutoMapperMappingException>(async () =>
            await _sut.AddFamilyMemberAsync(familyMemberDto));
        
        _familyMemberRepository.Verify(mock => mock.AddAsync(It.IsAny<FamilyMember>()), Times.Never);
    }
    
    [Fact]
    public async Task UpdateFamilyMemberAsync_Should_Call_RetrieveByIdAsync_And_UpdateAsync_When_Successful()
    {
        var fixture = new Fixture();
        var familyMember = fixture.Build<FamilyMember>()
            .Create();
        var familyMemberDto = fixture.Build<FamilyMemberDto>()
            .Create();

        _familyMemberRepository
            .Setup(mock => mock.RetrieveByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(familyMember);

        var result = await _sut.UpdateFamilyMemberAsync(familyMemberDto);

        result.Should().NotBeNull();
        _familyMemberRepository.Verify(mock => mock.RetrieveByIdAsync(It.IsAny<string>()), Times.Once);
        _familyMemberRepository.Verify(mock => mock.UpdateAsync(It.IsAny<FamilyMember>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateFamilyMemberAsync_Should_NotCall_UpdateAsync_When_RetrieveByIdAsyncReturnsNull()
    {
        var fixture = new Fixture();
        FamilyMember familyMember = null;
        var familyMemberDto = fixture.Build<FamilyMemberDto>()
            .Create();

        _familyMemberRepository
            .Setup(mock => mock.RetrieveByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(familyMember);

        await Assert.ThrowsAsync<Exception>(async () =>
            await _sut.UpdateFamilyMemberAsync(familyMemberDto));

        _familyMemberRepository.Verify(mock => mock.RetrieveByIdAsync(It.IsAny<string>()), Times.Once);
        _familyMemberRepository.Verify(mock => mock.UpdateAsync(It.IsAny<FamilyMember>()), Times.Never);
    }
    
    [Fact]
    public void DeleteFamilyMember_Should_Call_DeleteMethod()
    {
        _sut.DeleteFamilyMember(It.IsAny<string>());

        _familyMemberRepository.Verify(mock => mock.Delete(It.IsAny<string>()), Times.Once);
    }
}