using PROJECT_2024.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROJECT_2024.Dal
{

    public class CategoryDal: DbContext
    {
       // public CategoryDal(): base("CategoryString") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("tblCategory");
        }
       
        public DbSet<Category> Categorys { get; set; }

        public System.Data.Entity.DbSet<PROJECT_2024.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<PROJECT_2024.Models.Product> Products { get; set; }
    }

}