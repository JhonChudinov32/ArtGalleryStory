using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ArtGalleryStore.Domain.Entities
{
    public class ShippingDetails
    {
      
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите свой E-MAIL")]
        [Display(Name = "Электронный адрес")]
        public string Line1 { get; set; }
        [Required(ErrorMessage = "Укажите телефон")]
        [Display(Name = "Номер телефона")]
        public string Line2 { get; set; }
        [Display(Name = "Почтовый индекс")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
