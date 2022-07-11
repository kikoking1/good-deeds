namespace GoodDeeds.Core.Dtos;

public class FamilyMemberDto
{
    public string? Id { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int GoodDeedPoints { get; set; } = 0;
}