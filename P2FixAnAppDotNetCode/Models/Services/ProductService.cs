﻿using P2FixAnAppDotNetCode.Models.Repositories;
using System.Collections.Generic;
using System.Linq;


namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;


        private static List<Product> _products;

        public ProductService()
        {
        }

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all product from the inventory
        /// </summary>
        public Product[] GetAllProducts()
        {
            // TODO change the return type from array to List<T> and propagate the change
            // throughout the application
            return _productRepository.GetAllProducts();
        }

        /// <summary>
        /// Get a product form the inventory by its id
        /// </summary>
        public Product GetProductById(int idProduct)
        {
            // TODO implement the method (1 - En cours)

            return _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == idProduct);
        }

        /// <summary>
        /// Update the quantities left for each product in the inventory depending of ordered the quantities
        /// </summary>
        public void UpdateProductQuantities(Cart cart)
        {
            // TODO implement the method
            // update product inventory by using _productRepository.UpdateProductStocks() method.
            foreach (var pid in cart.Lines)
            {
                int productItem = pid.Product.Id;
                int qteItem = pid.Quantity;

                _productRepository.UpdateProductStocks(productItem, qteItem);
            }
        }
    }
}
