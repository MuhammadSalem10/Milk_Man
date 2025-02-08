

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;

namespace MilkMan.Infrastructure.DataAccess.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Apply unique constraints using Fluent API
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.PhoneNumber).IsUnique();

        // Apply other property configurations
        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.Email)
               .IsRequired();

        builder.Property(u => u.PhoneNumber)
               .IsRequired();
    }
}