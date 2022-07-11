namespace GoodDeeds.Core.Entities;

public class FamilyMember
{
    public string Id { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public int GoodDeedPoints { get; set; } = 0;
    
    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}