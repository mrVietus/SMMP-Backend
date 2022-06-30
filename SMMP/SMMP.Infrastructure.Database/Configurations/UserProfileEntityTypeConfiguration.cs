using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMMP.Core.Models.Authentication;
using SMMP.Core.Models.Enums;

namespace SMMP.Infrastructure.Database.Configurations
{
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
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
              .Property(up => up.FirstName);

            builder
              .Property(up => up.LastName);

            builder
               .Property(up => up.Created)
               .IsRequired();

            builder
               .Property(up => up.Updated);

            builder
              .Property(e => e.Status)
              .HasConversion(new EnumToStringConverter<UserStatus>())
              .IsRequired(true)
              .HasMaxLength(35);

            builder
              .Property(up => up.PhotoUrl);

            builder
                .HasOne(u => u.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(p => p.UserId);
        }
    }
}
