using GoodDeeds.Core.Entities;
using GoodDeeds.Core.Interfaces.Repositories;
using GoodDeeds.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GoodDeeds.Infrastructure.Repositories;

public class FamilyMemberRepository : IFamilyMemberRepository
{
    private readonly GoodDeedsDbContext _goodDeedsDbContext;
    
    public FamilyMemberRepository(GoodDeedsDbContext goodDeedsDbContext)
    {
        _goodDeedsDbContext = goodDeedsDbContext;
    }

    public async Task<FamilyMember?> RetrieveByIdAsync(string id)
    {
        return await _goodDeedsDbContext.FamilyMembers
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }
    
    public async Task<List<FamilyMember>> RetrieveAllAsync()
    {
        return await _goodDeedsDbContext.FamilyMembers.ToListAsync();
    }
    
    public async Task AddAsync(FamilyMember familyMember)
    {
        familyMember.Id = Guid.NewGuid().ToString();
        familyMember.DateCreated = DateTime.UtcNow;
        familyMember.DateModified = DateTime.UtcNow;

        await _goodDeedsDbContext.AddAsync(familyMember);
        await _goodDeedsDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(FamilyMember familyMember)
    {
        familyMember.DateModified = DateTime.UtcNow;

        _goodDeedsDbContext.Update(familyMember);
        await _goodDeedsDbContext.SaveChangesAsync();
    }
    
    public void Delete(string id)
    {
        _goodDeedsDbContext.Remove(_goodDeedsDbContext.FamilyMembers.Single(a => a.Id == id));
        _goodDeedsDbContext.SaveChanges();
    }
    
}