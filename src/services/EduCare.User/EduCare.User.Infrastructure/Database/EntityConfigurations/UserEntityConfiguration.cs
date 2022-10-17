using EduCare.User.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCare.User.Infrastructure.Database.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User.Core.Entities.User>
{
    public void Configure(EntityTypeBuilder<Core.Entities.User> builder)
    {
        builder.ToTable("Users", "User");

        builder.HasIndex(o => o.Email)
            .IsUnique();

        builder.Property(l => l.Email)
            .IsRequired();

        //     var users = ImportUsers();
        //     builder.HasData(users);
    }

    private IEnumerable<User.Core.Entities.User> ImportUsers()
    {
        var file = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "users.csv"));
        var users = new List<User.Core.Entities.User>();

        foreach (var line in file)
        {
            var data = line.Split(',');

            var user = new EduCare.User.Core.Entities.User
            {
                Id = int.Parse(data[0]),
                Email = data[1],
                Password = data[2],
                FullName = data[3],
                BirthDate = DateTime.Parse(data[4]),
                CreateDate = DateTime.Parse(data[5]),
                UpdateDate = DateTime.Parse(data[6]),
                UserType = (UserType)int.Parse(data[7]),
                Status = BuildingBlocks.Enums.StatusEnumType.active
            };

            if (users.Exists(l => l.Email.Equals(user.Email))) continue;
            users.Add(user);
        }

        return users;
    }
}