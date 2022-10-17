using EduCare.User.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCare.User.Infrastructure.Database.EntityConfigurations;

public class UserContactInformationEntityConfiguration : IEntityTypeConfiguration<UserContactInformation>
{
    public void Configure(EntityTypeBuilder<UserContactInformation> builder)
    {
        builder.ToTable("UsersContactInformations","User");

        builder.HasOne(l => l.User)
            .WithMany(l => l.UsersContactInformations)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(l => l.Address)
            .IsRequired();

        builder.Property(l => l.ContactType)
            .IsRequired();

        builder.HasIndex(l => new { l.Address, l.ContactType })
            .IsUnique();
        builder.HasIndex(l => new { l.Address, l.ContactType, l.IsPrimary });
    }
}
