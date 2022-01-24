using System.Collections.Generic;
using ArtGalleryStore.Domain.Entities;

namespace ArtGalleryStore.WebUI.Models
{
    public  class ArtGallerysListViewModel
    {
        public  IEnumerable <ArtGallery>ArtGallerys { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}