using EduCare.User.Core.Enums;

namespace EduCare.User.Core.Entities;

public class UserContactInformation : BuildingBlocks.Entity.Entity
{
    public ContactInformationType ContactType { get; set; }
    public string Address { get; set; }
    public int UserId { get; set; }
    public bool IsPrimary { get; set; }
    public User User { get; set; }
}