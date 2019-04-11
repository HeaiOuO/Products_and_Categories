using Microsoft.EntityFrameworkCore;
using ProAndCate.Models;
using System;

namespace ProAndCate.Models
{
    public class productContext :DbContext
    {
        public productContext(DbContextOptions<productContext> options) : base(options) {}

        public DbSet<Product> products{get;set;}
        public DbSet<Category> categories{get;set;}
        public DbSet<Association> associations{get;set;}


    }
}