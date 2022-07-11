using GoodDeeds.Core.Dtos;

namespace GoodDeeds.Core.Interfaces.Services;

public interface IFamilyMemberService
{
    public Task<FamilyMemberDto?> RetrieveFamilyMemberByIdAsync(string id);

    public Task<List<FamilyMemberDto>> RetrieveFamilyMembersAsync();
    
    public Task<FamilyMemberDto> AddFamilyMemberAsync(FamilyMemberDto familyMemberDto);
    
    public Task<FamilyMemberDto> UpdateFamilyMemberAsync(FamilyMemberDto familyMemberDto);
    
    public void DeleteFamilyMember(string id);
}