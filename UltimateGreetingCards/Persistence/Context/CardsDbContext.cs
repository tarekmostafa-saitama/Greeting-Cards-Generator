using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltimateGreetingCards.Core.DbEntities;

namespace UltimateGreetingCards.Persistence.Context
{
    public class CardsDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Card> Cards { get; set; }
        public CardsDbContext(DbContextOptions<CardsDbContext> options) : base(options)
        {
            
        }
    }
}
