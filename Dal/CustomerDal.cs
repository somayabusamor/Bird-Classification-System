using PROJECT_2024.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PROJECT_2024.Dal
{
    public class CustomerDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("tblCustomer");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}