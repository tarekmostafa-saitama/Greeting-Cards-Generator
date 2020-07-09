using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UltimateGreetingCards.Core;
using UltimateGreetingCards.Models;

namespace UltimateGreetingCards.Controllers
{
    public class AppController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AppController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [Route("Categories/{catUrl}")]
        [Route("Categories")]
        public IActionResult Categories(string catUrl)
        {
            if (string.IsNullOrEmpty(catUrl))
            {
                var cards = _unitOfWork.CardRepository.GetAll(new string[0]).ToList();
                return View(cards);
            }
            else
            {
                var cat = _unitOfWork.CategoryRepository.Find(x=>x.Url == catUrl,new []{"Cards"}).FirstOrDefault();
                if(cat !=null)
                    return View(cat.Cards.ToList());
                return View();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll(new string[0]).ToList();
            base.OnActionExecuting(context);
        }
        [Route("Card/{cardUrl}")]
        public IActionResult Card(string cardUrl)
        {
            var card = _unitOfWork.CardRepository.Find(x => x.Url == cardUrl, new string[0]).FirstOrDefault();
            if (card == null)
            {

            }

            card.ViewsCount += 1;
            _unitOfWork.Complete();
            return View();
        }
        [Route("Card/{cardUrl}")]
        [HttpPost]
        public IActionResult Card(string cardUrl,string name)
        {
            var card = _unitOfWork.CardRepository.Find(x => x.Url == cardUrl, new string[0]).FirstOrDefault();
            if (card == null)
            {

            }

            card.DownloadedCount += 1;
            _unitOfWork.Complete();


            var collection = new PrivateFontCollection();
            collection.AddFontFile(_hostEnvironment.WebRootPath+"/fonts/" + FontFamilyBinder.GetFontFamily(card.FontFamily));

            //creating a image object    
            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(_hostEnvironment.WebRootPath + "/Cards/" + card.ImagePath); // set image     
            //draw the image object using a Graphics object    
            Graphics graphicsImage = Graphics.FromImage(bitmap);

            //Set the alignment based on the coordinates       
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;



            //Set the font color/format/size etc..      
            Color StringColor = System.Drawing.ColorTranslator.FromHtml(card.FontColor);//direct color adding    

            string Str_TextOnImage = name;//Your Text On Image    


            graphicsImage.DrawString(Str_TextOnImage, new Font(collection.Families[0], card.FontSize,
                    FontStyle.Regular), new SolidBrush(StringColor), new Point(card.X, card.Y),
                stringformat); Response.ContentType = "image/png";



            bitmap.Save(_hostEnvironment.WebRootPath + "/card.png");

            string filePath = _hostEnvironment.WebRootPath + "/card.png";
            string fileName = name + ".png";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }
    }
}