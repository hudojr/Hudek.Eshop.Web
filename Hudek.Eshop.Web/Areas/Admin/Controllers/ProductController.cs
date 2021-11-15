using Hudek.Eshop.Web.Models.Database;
using Hudek.Eshop.Web.Models.Entity;
using Hudek.Eshop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hudek.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        readonly EshopDbContext eshopDbContext;
        IWebHostEnvironment env;

        public ProductController(EshopDbContext eshopDb, IWebHostEnvironment env)
        {
            eshopDbContext = eshopDb;
            this.env = env;
        }


        public IActionResult Select()
        {
            IndexViewModels indexVM = new IndexViewModels();

            indexVM.ProductItems = eshopDbContext.ProductItems.ToList();

            return View(indexVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductItem productItem)
        {
            if (String.IsNullOrEmpty(productItem.Name) == false)
            {
                /*if (DatabaseFake.ProductItems != null && DatabaseFake.ProductItems.Count > 0)
                {
                    productItem.ID = DatabaseFake.ProductItems.Last().ID + 1;
                }*/
                eshopDbContext.ProductItems.Add(productItem);

                await eshopDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(ProductController.Select));
            }
            else
            {
                return View(productItem);
            }


        }
        public IActionResult Edit(int ID)
        {
            ProductItem productItem = eshopDbContext.ProductItems.FirstOrDefault(ci => ci.ID == ID);

            if (productItem != null)
            {
                return View(productItem);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductItem pItem)
        {

            ProductItem productItem = eshopDbContext.ProductItems.FirstOrDefault(ci => ci.ID == pItem.ID);

            if (productItem != null)
            {
                if (String.IsNullOrEmpty(productItem.Name) == false
                   && String.IsNullOrEmpty(productItem.Description) == false)
                {
                    productItem.Name = pItem.Name;
                    productItem.Description = pItem.Description;

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProductController.Select));
                }
                else
                {
                    return View(productItem);
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Delete(int ID)
        {
            ProductItem productItem = eshopDbContext.ProductItems
                                                    .Where(ci => ci.ID == ID)
                                                    .FirstOrDefault();

            if (productItem != null)
            {
                eshopDbContext.ProductItems.Remove(productItem);

                await eshopDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ProductController.Select));
        }
    }
}
