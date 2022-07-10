using AutoMapper;
using GoodDeeds.Core.Dtos;
using GoodDeeds.Core.Entities;

namespace GoodDeeds.API.MapperProfiles;

public class FamilyMemberDtoMapping : Profile
{
    public FamilyMemberDtoMapping()
    {
        CreateMap<FamilyMember, FamilyMemberDto>();
        CreateMap<FamilyMemberDto, FamilyMember>();
    }
}