using System.Linq;
using System.Web.Mvc;
using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.WebUI.Models;


namespace ArtGalleryStore.WebUI.Controllers
{
    public class ArtGalleryController : Controller
    {
         private IArtGalleryRepository repository;
         public int pageSize = 4;
         public ArtGalleryController(IArtGalleryRepository repo)
         {
             repository = repo;
         }
         public ViewResult List(string category, int page = 1)
           {
               ArtGallerysListViewModel model = new ArtGallerysListViewModel
               {
                   ArtGallerys = repository.ArtGallerys
                       .Where(p => category == null || p.Category == category)
                       .OrderBy(ArtGallery => ArtGallery.Articl_Id)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize),
                   PagingInfo = new PagingInfo
                   {
                       CurrentPage = page,
                       ItemsPerPage = pageSize,
                       TotalItems = category == null ?
                       repository.ArtGallerys.Count() :
                       repository.ArtGallerys.Where(ArtGallery => ArtGallery.Category == category).Count()
                   },
                   CurrentCategory = category
               };
               return View(model);
           }
         public FileContentResult GetImage(int ArticlID)
         {
               ArtGallery art = repository.ArtGallerys
                   .FirstOrDefault(g => g.Articl_Id == ArticlID);

               if (art != null)
               {
                   return File(art.Photo, art.ImageMimeType);
               }
               else
               {
                   return null;
               }
         }

    }
}