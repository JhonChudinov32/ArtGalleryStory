using ArtGalleryStore.Domain.Entities;
using System.Data.Entity;

namespace ArtGalleryStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<ArtGallery> ArtGallerys { get; set; }
        
    }
}
