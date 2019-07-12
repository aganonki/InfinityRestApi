using System;
using InfinityRest.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace InfinityRest.Data.Helpers
{
    public static class ModelBuilderExtension
    {
        public static void AddEnum<TEntity, TEnum>(this ModelBuilder modelBuilder) where TEntity : BaseEntity, new()
        {
            foreach (TEnum model in (TEnum[])Enum.GetValues(typeof(TEnum)))
            {
                modelBuilder.Entity<TEntity>().HasData(new TEntity() { Id = model.GetHashCode(), Name = model.ToString()} );
            }
        }
    }
}
