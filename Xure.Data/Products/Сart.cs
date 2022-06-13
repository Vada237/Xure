using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xure.Data
{
   public class Cart
   {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(x => x.Product.Id == product.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.Id == product.Id);
        }

        public virtual decimal TotalValue()
        {
            return lineCollection.Sum(c => c.Product.Price.PriceHistory.Value * c.Quantity);
        }
        public virtual void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }

    public class CartLine {

        public Product Product;
        public int Quantity;
    }
}
