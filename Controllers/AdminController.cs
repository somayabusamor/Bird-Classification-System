using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using PROJECT_2024.Models;
using System.Data.Entity.Infrastructure;
using PROJECT_2024.Dal;
using System.IO;
using System.Data.Entity;

namespace PROJECT_2024.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ProductsList()
        {
            ProductDal db = new ProductDal();
            List<Product> products = db.Products.ToList();
            return View(products);
        }
        public ActionResult CategorysList()
        {
            CategoryDal db = new CategoryDal();
            List<Category> Categorys = db.Categorys.ToList();
            return View(Categorys);
        }
        public ActionResult CustomersList()
        {
            CustomerDal db = new CustomerDal();
            List<Customer> Customers = db.Customers.ToList();
            return View(Customers);
        }
        public ActionResult Showhomepage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {

            return View("SignUp", new Customer());
        }


        [HttpPost]
        public ActionResult AddCustomer(Customer cust)
        {
            using (CustomerDal dal = new CustomerDal())
            {
                if (ModelState.IsValid)
                {
                    if (dal.Customers.Any(x => x.Name == cust.Name))
                    {
                        // If the username already exists, return to the sign-up page with a duplicate message
                        ViewBag.DuplicateMessage = "Username already exists.";
                        return View("SignUp", cust);
                    }
                    if (Request.Form["IsAdmin"] != null)
                    {
                        if (Request.Form["IsAdmin"] == "true")
                        {
                            // If the checkbox is checked, set IsAdmin to true
                            cust.IsAdmin = true;
                        }
                        else
                        {
                            // Otherwise, set IsAdmin to false
                            cust.IsAdmin = false;
                        }
                    }


                    // Set IsAdmin to false by default

                    // Add the customer to the database
                    dal.Customers.Add(cust);

                    try
                    {
                        // Save changes to the database
                        dal.SaveChanges();
                        return View("LogIn");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Log the validation exception
                        foreach (var error in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in error.ValidationErrors)
                            {
                                Console.WriteLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                            }
                        }
                        // Optionally, handle or display the validation errors
                        return View("Error"); // Redirect to an error page or handle the error appropriately
                    }
                }
                else
                {
                    // If the model state is not valid, return to the sign-up page with the provided customer data
                    return View("SignUp", cust);
                }
            }
        }

        [HttpGet]
        public ActionResult EnterProduct()
        {
            
            return View("EnterProduct", new Product());
        }
        [HttpPost]
        public ActionResult AddProduct(Product prod)
        {
            if (ModelState.IsValid)
            {
                ProductDal dal = new ProductDal();
                CategoryDal Catdal = new CategoryDal();
                if (Catdal.Categorys.Any(c => c.CategoryID == prod.Category)) { 

                    dal.Products.Add(prod);
                try
                {
                    dal.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                }

                ModelState.Clear();

                return View("showhomepage");
                }
                else
                {
                    return View("Error");
                }
            }
            else
                return View("EnterProduct", prod);

        }
        [HttpGet]
        public ActionResult EnterCategory()
        {
            return View("EnterCategory", new Category());
        }
        [HttpPost]
        public ActionResult AddCategory(Category cate)
        {
            if (ModelState.IsValid)
            {
                CategoryDal dal = new CategoryDal();
                dal.Categorys.Add(cate);
                try
                {
                    dal.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                }
                return View("showhomepage");
            }
            else
                return View("EnterCategory", cate);
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            CategoryDal dal = new CategoryDal();
            Category category = dal.Categorys.Find(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                using (CategoryDal dal = new CategoryDal()) // Use 'using' statement for disposing DbContext
                {
                    dal.Entry(category).State = EntityState.Modified;
                    dal.SaveChanges();
                }
                return RedirectToAction("Showhomepage"); // Redirect to a suitable action after editing
            }
            return View(category);
        }


        [HttpGet]
        public ActionResult ConfirmDeleteCategory(int id)
        {
            using (CategoryDal dal = new CategoryDal())
            {
                Category categoryToDelete = dal.Categorys.Find(id);
                if (categoryToDelete != null)
                {
                    return View(categoryToDelete);
                }
                else
                {
                    // Category not found, return appropriate view or redirect
                    return RedirectToAction("Showhomepage");
                }
            }
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            using (CategoryDal dal = new CategoryDal())
            {
                Category categoryToDelete = dal.Categorys.Find(id);
                if (categoryToDelete != null)
                {
                    dal.Categorys.Remove(categoryToDelete);
                    dal.SaveChanges();
                }
                // Redirect to appropriate action after deletion
                return RedirectToAction("Showhomepage");
            }
        }

        public ActionResult DetailsCategory(int id)
        {
            CategoryDal dal = new CategoryDal();
            Category cut = dal.Categorys.Find(id);
            return View(cut);
        }
        public ActionResult ViewProductCard()
        {
            return View("ViewProductCard");
        }

        public ActionResult EditProduct(int id)
        {
            ProductDal dal = new ProductDal();
            Product product = dal.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                using (ProductDal dal = new ProductDal()) // Use 'using' statement for disposing DbContext
                {
                    dal.Entry(product).State = EntityState.Modified;
                    dal.SaveChanges();
                }
                return RedirectToAction("Showhomepage"); // Redirect to a suitable action after editing
            }
            return View(product);
        }
        public ActionResult DetailsProduct(int id)
        {
            ProductDal dal = new ProductDal();
            Product product = dal.Products.Find(id);
            return View(product);
        }
        public ActionResult shoppingCard()
        {
            return View();

        }
        [HttpGet]
        public ActionResult ConfirmDeleteProduct(int id)
        {
            using (ProductDal dal = new ProductDal())
            {
                Product productToDelete = dal.Products.Find(id);
                if (productToDelete != null)
                {
                    return View(productToDelete);
                }
                else
                {
                    // Category not found, return appropriate view or redirect
                    return RedirectToAction("Showhomepage");
                }
            }
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            using (ProductDal dal = new ProductDal())
            {
                Product productToDelete = dal.Products.Find(id);
                if (productToDelete != null)
                {
                    dal.Products.Remove(productToDelete);
                    dal.SaveChanges();
                }
                // Redirect to appropriate action after deletion
                return RedirectToAction("Showhomepage");
            }
        }

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source=LAPTOP-SOMAYA; database=tempdb; Integrated Security=True;";
        }

        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;

            // Use parameterized queries to prevent SQL injection
            com.CommandText = "SELECT * FROM tblCustomer WHERE Name=@Name AND Password=@Password";
            com.Parameters.AddWithValue("@Name", acc.Name);
            com.Parameters.AddWithValue("@Password", acc.Password);
            bool isAdmin = Convert.ToBoolean(dr["IsAdmin"]);

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                ViewBag.SuccessMessage = "Login successful!";
                if (isAdmin)
                {
                    return RedirectToAction("ShowhomePage", "Admin");
                }
                else
                {
                    return RedirectToAction("OurProducts", "Customer");
                }
            }
            else
            {
                con.Close();
                return View("Error404");
            }
        }



    }

}
