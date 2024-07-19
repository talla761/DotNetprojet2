using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        public List<CartLine> _addPanier = new List<CartLine>();
        private int orderLineId = 0;

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return _addPanier; // new List<CartLine>();
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {

            // TODO implement the method (2 - En cours)
            Product existingItem = FindProductInCartLines(product.Id);
            
            //_addPanier.Where(p => p.Product == product).ToList();

            //Product product1 = _addPanier.Where(p => p.Product == product); ;

            if (existingItem == null)
            {
                _addPanier.Add(new CartLine() { OrderLineId = orderLineId, Product = product, Quantity = quantity });
                orderLineId++;

            }
            else
            {
                var getTheQuantity = _addPanier.FirstOrDefault(p => p.Product == existingItem).Quantity;
                var getTheOrderLineId = _addPanier.FirstOrDefault(p => p.Product == existingItem).OrderLineId;

                getTheQuantity++;

                //var itemToRemove = _addPanier.Single(r => r.OrderLineId == 2);

                _addPanier.RemoveAt(getTheOrderLineId);
                _addPanier.Add(new CartLine() { OrderLineId = getTheOrderLineId, Product = existingItem, Quantity = getTheQuantity });


            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method

            double results = 0;
            foreach (var itemCart in _addPanier)
            {
                results += (itemCart.Quantity * itemCart.Product.Price);
            }
            return results;

        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            //double results = _addPanier.Select(a => a.Product.Price).ToList().Average();
            double results  = _addPanier.Average(a => a.Product.Price);
            return results;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method (3 - OK)

            //var existingItem = _addPanier.Where(p => p.Product.Id == productId).ToList();

            foreach (var line in _addPanier)
            {
                if(line.Product.Id == productId)
                {
                    return line.Product;
                }

            }

            return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
