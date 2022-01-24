
using System.Linq;
using System.Web.Mvc;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.WebUI.Models;


namespace ArtGalleryStore.WebUI.Controllers
{
    public class CartController : Controller
    {
       
        private IArtGalleryRepository repository;
        private IOrderProcessor orderProcessor;
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public CartController(IArtGalleryRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        } 
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int articl_id, string returnUrl)
        {
            ArtGallery art = repository.ArtGallerys
                .FirstOrDefault(g => g.Articl_Id == articl_id);

            if (art != null)
            {
                cart.AddItem(art, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int articl_id, string returnUrl)
        {
            ArtGallery art = repository.ArtGallerys
                .FirstOrDefault(g => g.Articl_Id == articl_id);

            if (art != null)
            {
                cart.RemoveLine(art);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
       
    }
}