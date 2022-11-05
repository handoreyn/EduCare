using System.ComponentModel.DataAnnotations;

namespace EduCare.User.Core.Dtos;

public class AddUserDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    public string FullName { get; set; }
}