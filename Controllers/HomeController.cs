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
    public class HomeController : Controller
    {
        // GET: Home
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
        public ActionResult Verify(Customer acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;

            // Use parameterized queries to prevent SQL injection
            com.CommandText = "SELECT * FROM tblCustomer WHERE Name=@Name AND Password=@Password";
            com.Parameters.AddWithValue("@Name", acc.Name);
            com.Parameters.AddWithValue("@Password", acc.Password);

            // Execute the command and read the data
            dr = com.ExecuteReader();

            if (dr.Read())
            {
                // Read IsAdmin after checking if there are any rows returned by the query
                bool isAdmin = Convert.ToBoolean(dr["IsAdmin"]);

                con.Close();

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