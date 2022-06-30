using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SMMP.Core.Models;
using SMMP.Core.Models.Enums;

namespace SMMP.Infrastructure.Database.Configurations
{
    public class ExecutionEntityTypeConfiguration : IEntityTypeConfiguration<Execution>
    {
        public void Configure(EntityTypeBuilder<Execution> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
             .Property(e => e.Name)
             .IsRequired(true)
             .HasMaxLength(35);

            builder
               .Property(e => e.Status)
               .HasConversion(new EnumToStringConverter<ExecutionStatus>())
               .IsRequired(true)
               .HasMaxLength(35);

            builder
                .Property(e => e.StartDate)
                .IsRequired(true)
                .HasDefaultValueSql("getdate()");

            builder
                .Property(e => e.LastChangeDate)
                .IsRequired(true)
                .HasDefaultValueSql("getdate()");

            builder
             .Property(e => e.LogInfo);

            builder
                .HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(p => p.ParentId);
        }
    }
}
