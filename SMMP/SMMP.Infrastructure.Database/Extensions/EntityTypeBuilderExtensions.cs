using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SMMP.Infrastructure.Database.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> AddEnumConstraint<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder, string columnName)
            where TEntity : class, new()
            where TEnum : Enum
        {
            var enumJoinedValues = string.Join("','", Enum.GetNames(typeof(TEnum)));

            var constraintName = $"CHK_{typeof(TEntity).Name}_{columnName}";
            var constraint = $"{columnName} IN ('{enumJoinedValues}')";

            builder.HasCheckConstraint(constraintName, constraint);

            return builder;
        }
    }
}
