using PROJECT_2024.Dal;
using PROJECT_2024.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJECT_2024.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult OurProducts()
        {
            ProductDal db = new ProductDal();
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        public ActionResult AddToCart(int productId)
        {
            using (ProductDal dal = new ProductDal())
            {
                var product = dal.Products.FirstOrDefault(p => p.PId == productId);
                if (product != null && product.Quantity > 0)
                {
                    // Use CartItem instead of Product
                    List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
                    if (cartItems == null)
                    {
                        cartItems = new List<CartItem>();
                        Session["CartItems"] = cartItems;
                    }

                    // Check for existing item in the cart (optional for quantity update)
                    var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity++;
                        existingItem.Price = existingItem.Product.Price * existingItem.Quantity;

                    }
                    else
                    {
                        cartItems.Add(new CartItem { ProductId = productId, Product = product, Quantity = 1 });
                    }

                    product.Quantity--;
                    dal.SaveChanges();
                }
            }
            return RedirectToAction("OurProducts");
        }

        public ActionResult ShoppingCart()
        {

            // Retrieve cart items from session
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;
            return View(cartItems);
        }
        [HttpGet]
        public ActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cartItems = Session["CartItems"] as List<CartItem>;

            if (cartItems != null)
            {
                CartItem productToRemove = cartItems.FirstOrDefault(p => p.Product.PId == productId);

                if (productToRemove != null)
                {
                    if (productToRemove.Quantity > 1)
                    {
                        // If quantity is greater than 1, decrement the quantity by 1
                        productToRemove.Quantity--;

                        // Recalculate the total price based on the updated quantity
                        productToRemove.Price = productToRemove.Product.Price * productToRemove.Quantity;
                    }
                    else
                    {
                        // If quantity is 1, remove the product from the cart
                        cartItems.Remove(productToRemove);
                    }
                    Session["CartItems"] = cartItems;
                }
            }

            return RedirectToAction("ShoppingCart");
        }






    }
}