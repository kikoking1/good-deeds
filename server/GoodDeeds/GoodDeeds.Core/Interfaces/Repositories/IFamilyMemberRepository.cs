using GoodDeeds.Core.Entities;

namespace GoodDeeds.Core.Interfaces.Repositories;

public interface IFamilyMemberRepository
{
    public Task<FamilyMember?> RetrieveByIdAsync(string id);

    public Task<List<FamilyMember>> RetrieveAllAsync();
    
    public Task AddAsync(FamilyMember familyMember);

    public Task UpdateAsync(FamilyMember familyMember);

    public void Delete(string id);
}