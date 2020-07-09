using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateGreetingCards.Core;
using UltimateGreetingCards.Core.Repositories;
using UltimateGreetingCards.Persistence.Context;
using UltimateGreetingCards.Persistence.Repositories;

namespace UltimateGreetingCards.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CardsDbContext _context;
        public ICategoryRepository CategoryRepository { get; set; }
        public ICardRepository CardRepository { get; set; }

        public UnitOfWork(CardsDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            CardRepository = new CardRepository(context);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
