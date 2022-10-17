
using EduCare.User.Core.Enums;

namespace EduCare.User.Core.Entities;

public class User : BuildingBlocks.Entity.Entity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public UserType UserType { get; set; }
    public ICollection<UserContactInformation> UsersContactInformations { get; set; }

    public User()
    {
        UsersContactInformations = new List<UserContactInformation>();
    }
}