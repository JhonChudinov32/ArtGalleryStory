using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGalleryStore.Domain.Abstract;

namespace ArtGalleryStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IArtGalleryRepository repository;

        public NavController(IArtGalleryRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.ArtGallerys
                .Select(ArtGalleryStore => ArtGalleryStore.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}