using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltimateGreetingCards.Core.DbEntities;
using UltimateGreetingCards.Core.Repositories;

namespace UltimateGreetingCards.Persistence.Repositories
{
    public class CardRepository : Repository<Card, string>, ICardRepository
    {
        private readonly DbContext _context;

        public CardRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
