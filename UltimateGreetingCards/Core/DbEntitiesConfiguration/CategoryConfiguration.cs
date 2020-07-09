using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltimateGreetingCards.Core.DbEntities;

namespace UltimateGreetingCards.Core.DbEntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Cards).WithOne(c => c.Category).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
