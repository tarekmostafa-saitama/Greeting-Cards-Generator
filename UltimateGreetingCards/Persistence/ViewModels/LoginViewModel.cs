using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateGreetingCards.Persistence.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "اسم المستخدم")]
        [Required(ErrorMessage = " اسم المستخدم مطلوب.")]
        public string UserName { get; set; }
        [Display(Name = "الرقم السرى")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "الرقم السرى مطلوب.")]
        public string Password { get; set; }

        [Display(Name = "تذكرنى ؟")]
        public bool RememberMe { get; set; }
    }
}
