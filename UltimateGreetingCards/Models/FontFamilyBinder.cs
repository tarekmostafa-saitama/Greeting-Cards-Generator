using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateGreetingCards.Core.Enums;

namespace UltimateGreetingCards.Models
{
    public class FontFamilyBinder
    {
        public static string GetFontFamily(FontFamily fontFamily)
        {
            switch (fontFamily)
            {
                case FontFamily.الاميرى:
                    return "Amiri-Regular.ttf";

                case FontFamily.القاهرة:
                    return "Cairo-Regular.ttf";

                case FontFamily.تجوال:
                    return "Tajawal-Regular.ttf";

                case FontFamily.رقااص:
                    return "Rakkas-Regular.ttf";

                case FontFamily.ريم:
                    return "ReemKufi-Regular.ttf";

                case FontFamily.شانجا:
                    return "Changa-Regular.ttf";

                case FontFamily.فايبز:
                    return "Vibes-Regular.ttf";

                case FontFamily.كوفى:
                    return "DroidKufiRegular.ttf";

                case FontFamily.ليمونادا:
                    return "Lemonada-Regular.ttf";


                default:
                    return "DroidKufiRegular.ttf";
            }
        }
    }
}
