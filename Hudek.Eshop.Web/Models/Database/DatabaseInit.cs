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

            CarouselItem ci1 = new CarouselItem()
            {
                ID = 0,
                ImageSource = "/img/pic1.jpg",
                ImageAlt = "First slide"
            };

            CarouselItem ci2 = new CarouselItem()
            {
                ID = 1,
                ImageSource = "/img/pic2.jpg",
                ImageAlt = "Second slide"
            };

            CarouselItem ci3 = new CarouselItem()
            {
                ID = 2,
                ImageSource = "/img/pic3.jpg",
                ImageAlt = "Third slide"
            };

            carouselItems.Add(ci1);
            carouselItems.Add(ci2);
            carouselItems.Add(ci3);

            return carouselItems;
        }

        public List<ProductItem> GenerateProductItems()
        {
            List<ProductItem> ProductItems = new List<ProductItem>();

            ProductItem p1 = new ProductItem()
            {
                ID = 0,
                Name = "Krovinorez",
                Price = 15,
                Description = "Description of product1."
    };

            ProductItem p2 = new ProductItem()
            {
                ID = 1,
                Name = "Kosačka",
                Price = 22.3,
                Description = "Description of product2."
            };

            ProductItem p3 = new ProductItem()
            {
                ID = 2,
                Name = "Motorová píla",
                Price = 99.99,
                Description = "Description of product3."
            };

            ProductItem p4 = new ProductItem()
            {
                ID = 3,
                Name = "Pivo",
                Price = 1.15,
                Description = "Description of product4."
            };

            ProductItems.Add(p1);
            ProductItems.Add(p2);
            ProductItems.Add(p3);
            ProductItems.Add(p4);



            return ProductItems;
        }
    }
}
