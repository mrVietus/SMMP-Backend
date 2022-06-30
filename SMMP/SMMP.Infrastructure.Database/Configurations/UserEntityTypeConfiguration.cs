using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMMP.Core.Models.Authentication;


namespace SMMP.Infrastructure.Database.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder
               .Property(e => e.Identifier)
               .IsRequired()
               .HasMaxLength(64);

            builder
               .Property(up => up.Email)
               .HasMaxLength(100)
               .IsRequired();

            builder
               .Property(up => up.Password)
               .HasMaxLength(50)
               .IsRequired();

            builder
              .Property(up => up.Salt)
              .IsRequired();

            builder
              .Property(up => up.IsActive);

            builder
             .Property(up => up.UserRole);

            builder
                .HasOne(u => u.Profile)
                .WithOne(u => u.User);
        }
    }
}
