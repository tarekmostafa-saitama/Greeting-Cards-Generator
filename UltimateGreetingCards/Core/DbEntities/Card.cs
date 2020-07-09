using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateGreetingCards.Core.Enums;

namespace UltimateGreetingCards.Core.DbEntities
{
    public class Card
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Url { get; set; }
        public FontFamily FontFamily { get; set; }
        public int FontSize { get; set; }
        public string FontColor { get; set; }
        public int DownloadedCount { get; set; }
        public int ViewsCount { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
