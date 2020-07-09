using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateGreetingCards.Core.Repositories;

namespace UltimateGreetingCards.Core
{
    public interface IUnitOfWork
    {
         ICategoryRepository CategoryRepository { get; set; }
         ICardRepository CardRepository { get; set; }

         void Complete();
    }
}
