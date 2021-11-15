using Hudek.Eshop.Web.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hudek.Eshop.Web.Models.Database
{
    public class EshopDbContext : DbContext
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }


        public EshopDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
