using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateGreetingCards.Core.DbEntities
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
