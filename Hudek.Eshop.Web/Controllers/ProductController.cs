using Hudek.Eshop.Web.Models.Database;
using Hudek.Eshop.Web.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hudek.Eshop.Web.Controllers
{
    public class ProductController : Controller
    {
        readonly EshopDbContext eshopDbContext;

        public ProductController(EshopDbContext eshopDb)
        {
            eshopDbContext = eshopDb;
        }
        public IActionResult Detail(int ID)
        {
                ProductItem productItem = eshopDbContext.ProductItems.FirstOrDefault(ci => ci.ID == ID);

                if (productItem != null)
                {
                    return View(productItem);
                }
                return NotFound();

        }
    }
}
