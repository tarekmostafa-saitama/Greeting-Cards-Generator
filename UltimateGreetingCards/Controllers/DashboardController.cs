using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UltimateGreetingCards.Core;
using UltimateGreetingCards.Core.DbEntities;
using UltimateGreetingCards.Models;
using UltimateGreetingCards.Persistence.ViewModels;

namespace UltimateGreetingCards.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DashboardController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
   
        [Route("Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            var model = new DashboardViewModel()
            {
                CardsCount = _unitOfWork.CardRepository.Count(null),
                CategoriesCount = _unitOfWork.CategoryRepository.Count(null)
            };
            return View(model);
        }
        // Categories Section
        [Route("Admin/Categories/List")]
        public IActionResult Categories()
        {
           var model =  _unitOfWork.CategoryRepository.GetAll(new[] {"Cards"});
           return View(model);
        }
        [HttpPost]
        [Route("Admin/Categories/Add")]
        public IActionResult AddCategory(string name)
        {
            var model = new Category()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Url = GetRandomString()
            };
            _unitOfWork.CategoryRepository.Add(model);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Categories));
        }
 
        [Route("Admin/Categories/Delete/{id}")]
        public IActionResult DeleteCategory(string id)
        {
            var model = _unitOfWork.CategoryRepository.Get(id, new string[0]);
            _unitOfWork.CategoryRepository.Delete(model);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Categories));
        }


        // Cards Section
        [Route("Admin/Cards/List/{id}")]
        public IActionResult Cards(string id)
        {
            var model = _unitOfWork.CardRepository.Find(x => x.CategoryId == id, new string[0]);
            return View(model);
        }
        [Route("Admin/Cards/Add")]
        public IActionResult AddCard()
        {
            ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll(new string[0]).ToList();
            return View();
        }
        [HttpPost]
        [Route("Admin/Cards/Add")]
        public IActionResult AddCard(AddCardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll(new string[0]).ToList();
                return View(model);
            }

            var newName = Guid.NewGuid();
            var path = _hostEnvironment.WebRootPath + "/cards/" + newName + model.Image.FileName;
            FileStream stream = new FileStream(path, FileMode.Create);
            model.Image.CopyTo(stream);
            stream.Close();
            var card = new Card()
            {
                Id = Guid.NewGuid().ToString(),
                FontColor = model.FontColor,
                FontSize = model.FontSize,
                FontFamily = model.FontFamily,
                X = model.X ?? default,
                Y = model.Y ?? default,
                DownloadedCount = 0,
                ViewsCount = 0,
                ImagePath = newName + model.Image.FileName,
                CategoryId = model.CategoryId,
                Url = GetRandomString()
            };
            _unitOfWork.CardRepository.Add(card);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Categories));
        }


        [Route("Admin/Cards/Update/{id}")]
        public IActionResult UpdateCard(string id)
        {
            ViewBag.Categories = _unitOfWork.CategoryRepository.GetAll(new string[0]).ToList();
            var model = _unitOfWork.CardRepository.Get(id, new string[0]);
            return View(model);
        }
        [HttpPost]
        [Route("Admin/Cards/Update")]
        public IActionResult EditCard(IFormFile Image, Card model)
        {

           
            var objectCard = _unitOfWork.CardRepository.Get(model.Id, new string[0]);
            objectCard.CategoryId = model.CategoryId;
            objectCard.X = model.X;
            objectCard.Y = model.Y;
            objectCard.FontColor = model.FontColor;
            objectCard.FontFamily = model.FontFamily;
            objectCard.FontSize = model.FontSize;
            if (Image != null)
            {
                var newName = Guid.NewGuid();
                var path = _hostEnvironment.WebRootPath + "/cards/" + newName + Image.FileName;
                FileStream stream = new FileStream(path, FileMode.Create);
                Image.CopyTo(stream);
                stream.Close();
                objectCard.ImagePath = newName + Image.FileName;
            }
            _unitOfWork.Complete();
            return RedirectToAction(nameof(UpdateCard),new {id=model.Id});
        }

        [Route("Admin/Cards/Delete/{id}")]
        public IActionResult DeleteCard(string id)
        {
            var model = _unitOfWork.CardRepository.Get(id, new string[0]);
            var catId = model.CategoryId;
            _unitOfWork.CardRepository.Delete(model);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Cards),new {id= catId});
        }


        #region Helpers

        private string GetRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        #endregion
    }
}