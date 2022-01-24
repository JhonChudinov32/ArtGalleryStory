using ArtGalleryStore.Domain.Entities;

namespace ArtGalleryStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
