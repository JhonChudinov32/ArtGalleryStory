using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.Domain.Entities;

namespace ArtGalleryStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IArtGalleryRepository repository;
        public AdminController(IArtGalleryRepository repo)
            {
                repository = repo;
            }
        public ViewResult Index()
            {
                return View(repository.ArtGallerys);
            }
        public ViewResult Edit(int articlId)
            {
                ArtGallery artGallery = repository.ArtGallerys
                    .FirstOrDefault(g => g.Articl_Id == articlId);
                return View(artGallery);
            }
            // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(ArtGallery artGallery, HttpPostedFileBase image = null)
            {
                if (ModelState.IsValid)
                {
           
                if (image != null)
                    {
                   
                     artGallery.ImageMimeType = image.ContentType;
                     artGallery.Photo = new byte[image.ContentLength];
                     image.InputStream.Read(artGallery.Photo, 0, image.ContentLength);
                    }
                    repository.SaveArt(artGallery);
                    TempData["message"] = string.Format("Изменения в картине \"{0}\" были сохранены", artGallery.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    // Что-то не так со значениями данных
                    return View(artGallery);
                }
            }
        public ViewResult Create()
            {
                return View("Edit", new ArtGallery());
            }
        [HttpPost]
        public ActionResult Delete(int ArticlId)
            {
                ArtGallery deletedArt = repository.DeleteArt(ArticlId);
                if (deletedArt != null)
                {
                    TempData["message"] = string.Format("Картина \"{0}\" была удалена",
                        deletedArt.Name);
                }
                return RedirectToAction("Index");
            }
    }
}