
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ArtGalleryStore.Domain.Entities
{
    public class ArtGallery
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Articl_Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название картины")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для картины")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для картины")]
        public string Category { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

        public byte[] Photo { get; set; }

        public string ImageMimeType { get; set; }

        
    }
}
