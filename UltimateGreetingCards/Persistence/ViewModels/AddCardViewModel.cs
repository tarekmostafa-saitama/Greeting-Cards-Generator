using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UltimateGreetingCards.Core.Enums;

namespace UltimateGreetingCards.Persistence.ViewModels
{
    public class AddCardViewModel
    {
        [Required(ErrorMessage = "صورة كارت المعايدة مطلوب")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "الاحداثى السينى مطلوب")]
        public int? X { get; set; }
        [Required(ErrorMessage = "الاحداثى الصادى مطلوب")]
        public int? Y { get; set; }

        public FontFamily FontFamily { get; set; }
        public int FontSize { get; set; }
        public string FontColor { get; set; }
        [Required(ErrorMessage = "حقل القسم مطلوب.")]
        public string CategoryId { get; set; }
    }
}
