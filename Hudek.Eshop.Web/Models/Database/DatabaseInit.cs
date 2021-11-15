using Hudek.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hudek.Eshop.Web.Models.Database
{
    public class DatabaseInit
    {

        public void Initialization(EshopDbContext eshopDbContext)
        {

            eshopDbContext.Database.EnsureCreated();

            if(eshopDbContext.CarouselItems.Count() == 0)
            {

                IList<CarouselItem> cItems = GenerateCarouselItems();
                foreach(var ci in cItems)
                {
                    eshopDbContext.CarouselItems.Add(ci);
                }


                IList<ProductItem> pItems = GenerateProductItems();
                foreach (var ci in pItems)
                {
                    eshopDbContext.ProductItems.Add(ci);
                }

                eshopDbContext.SaveChanges();
            }

        }

        public List<CarouselItem> GenerateCarouselItems()
        {
            List<CarouselItem> carouselItems = new List<CarouselItem>();


            return carouselItems;
        }

        public List<ProductItem> GenerateProductItems()
        {
            List<ProductItem> ProductItems = new List<ProductItem>();

            return ProductItems;
        }
    }
}
