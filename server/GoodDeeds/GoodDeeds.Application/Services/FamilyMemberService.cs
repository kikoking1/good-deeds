using AutoMapper;
using GoodDeeds.Core.Dtos;
using GoodDeeds.Core.Entities;
using GoodDeeds.Core.Interfaces.Repositories;
using GoodDeeds.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GoodDeeds.Application.Services;

public class FamilyMemberService : IFamilyMemberService
{
    private readonly IFamilyMemberRepository _familyMemberRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FamilyMemberService> _logger;

    public FamilyMemberService(
        IFamilyMemberRepository familyMemberRepository,
        IMapper mapper,
        ILogger<FamilyMemberService> logger)
    {
        _familyMemberRepository = familyMemberRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FamilyMemberDto?> RetrieveFamilyMemberByIdAsync(string id)
    {
        var familyMember = await _familyMemberRepository.RetrieveByIdAsync(id);
        var familyMemberDto = _mapper.Map<FamilyMemberDto>(familyMember);

        return familyMemberDto;
    }
    
    public async Task<List<FamilyMemberDto>> RetrieveFamilyMembersAsync()
    {
        var familyMembers = await _familyMemberRepository.RetrieveAllAsync();
        var familyMemberDtos = _mapper.Map<List<FamilyMemberDto>>(familyMembers);

        return familyMemberDtos;
    }
    
    public async Task AddFamilyMemberAsync(FamilyMemberDto familyMemberDto)
    {
        var familyMember = _mapper.Map<FamilyMember>(familyMemberDto);
        await _familyMemberRepository.AddAsync(familyMember);
    }
}