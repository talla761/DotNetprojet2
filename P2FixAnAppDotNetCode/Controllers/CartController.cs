using System.Linq;
using Microsoft.AspNetCore.Mvc;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly IProductService _productService;

        public CartController(ICart pCart, IProductService productService)
        {
            _cart = pCart;
            _productService = productService;
        }

        public ViewResult Index()
        {
            return View(_cart as Cart);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int id)
        {
            //ProductService repository = new ProductService();
            //var product = repository.GetProductById(id);

            Product product = _productService.GetProductById(id);

            if (product != null)
            {
                _cart.AddItem(product, 1);
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                //Product[] products = _productService.GetProductById(id);

                //return View(products);

                return RedirectToAction("Index", "Product");
            }
        }

        public RedirectToActionResult RemoveFromCart(int id)
        {
            Product product = _productService.GetAllProducts()
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index");
        }
    }
}
