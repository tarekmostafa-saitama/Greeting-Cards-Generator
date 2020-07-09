using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltimateGreetingCards.Core.DbEntities;
using UltimateGreetingCards.Core.Repositories;

namespace UltimateGreetingCards.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category, string>, ICategoryRepository
    {
        private readonly DbContext _context;

        public CategoryRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
