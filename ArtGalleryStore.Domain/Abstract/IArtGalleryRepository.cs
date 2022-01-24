
using ArtGalleryStore.Domain.Entities;
using System.Collections.Generic;

namespace ArtGalleryStore.Domain.Abstract
{
    public interface IArtGalleryRepository
    {
        IEnumerable<ArtGallery> ArtGallerys { get; }
        void SaveArt(ArtGallery art);
        ArtGallery DeleteArt(int Articl_id);
    }
}
