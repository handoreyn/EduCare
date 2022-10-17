using EduCare.User.Core.Entities;
using EduCare.User.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EduCare.User.Infrastructure.Database;

public class EduCareDatabaseContext : DbContext
{
    public EduCareDatabaseContext(DbContextOptions<EduCareDatabaseContext> options) : base(options)
    {
    }
    public DbSet<User.Core.Entities.User> Users { get; set; }
    public DbSet<UserContactInformation> UserContactInformations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserContactInformationEntityConfiguration());
    }
}