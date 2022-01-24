
using System.Collections.Generic;
using System.Linq;


namespace ArtGalleryStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(ArtGallery Art, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Art.Articl_Id == Art.Articl_Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Art = Art,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(ArtGallery Art)
        {
            lineCollection.RemoveAll(l => l.Art.Articl_Id == Art.Articl_Id);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Art.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
    public class CartLine
    {
        public ArtGallery Art { get; set; }
        public int Quantity { get; set; }
    }


}
