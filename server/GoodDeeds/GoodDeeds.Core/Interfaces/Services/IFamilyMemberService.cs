using GoodDeeds.Core.Dtos;

namespace GoodDeeds.Core.Interfaces.Services;

public interface IFamilyMemberService
{
    public Task<FamilyMemberDto?> RetrieveFamilyMemberByIdAsync(string id);

    public Task<List<FamilyMemberDto>> RetrieveFamilyMembersAsync();
    //
    public Task AddFamilyMemberAsync(FamilyMemberDto familyMemberDto);
    //
    // public Task UpdateAsync(FamilyMember familyMember);
    //
    // public void Delete(FamilyMember familyMember);
}