using System.Collections.Generic;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.Domain.Abstract;

namespace ArtGalleryStore.Domain.Concrete
{
    public class EFArtGalleryRepository : IArtGalleryRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<ArtGallery> ArtGallerys
        {
            get { return context.ArtGallerys; }
        }
        public void SaveArt(ArtGallery art)
        {
            if (art.Articl_Id == 0)
                context.ArtGallerys.Add(art);
            else
            {
                ArtGallery dbEntry = context.ArtGallerys.Find(art.Articl_Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = art.Name;
                    dbEntry.Description = art.Description;
                    dbEntry.Price = art.Price;
                    dbEntry.Category = art.Category;
                    dbEntry.Photo = art.Photo;
                    dbEntry.ImageMimeType = art.ImageMimeType;
                  
                }
            }
            context.SaveChanges();
        }
        public ArtGallery DeleteArt(int artId)
        {
            ArtGallery dbEntry = context.ArtGallerys.Find(artId);
            if (dbEntry != null)
            {
                context.ArtGallerys.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
