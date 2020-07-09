using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateGreetingCards.Core.DbEntities;

namespace UltimateGreetingCards.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category, string>
    {
        
    }
}
