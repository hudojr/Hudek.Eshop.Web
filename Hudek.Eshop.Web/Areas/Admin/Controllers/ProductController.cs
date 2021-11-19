using Hudek.Eshop.Web.Models.Database;
using Hudek.Eshop.Web.Models.Entity;
using Hudek.Eshop.Web.Models.Implementation;
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
            FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/Products", "image");

            if (fileUpload.CheckFileContent(productItem.Image)
                   && fileUpload.CheckFileLength(productItem.Image))
            {
                productItem.ImageSource450x300 = await fileUpload.FileUploadAsync(productItem.Image);

                ModelState.Clear();
                TryValidateModel(productItem);
                if (ModelState.IsValid)
                {
                    eshopDbContext.ProductItems.Add(productItem);

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProductController.Select));
                }
            }

            return View(productItem);
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

                if (pItem.Image != null)
                {
                    FileUpload fileUpload = new FileUpload(env.WebRootPath, "img/Products", "image");

                    if (fileUpload.CheckFileContent(pItem.Image)
                       && fileUpload.CheckFileLength(pItem.Image))
                    {

                        pItem.ImageSource450x300 = await fileUpload.FileUploadAsync(pItem.Image);
                        productItem.ImageSource450x300 = pItem.ImageSource450x300;

                    }
                }
                else
                {
                    pItem.ImageSource450x300 = "-";
                }


                ModelState.Clear();
                TryValidateModel(pItem);
                if (ModelState.IsValid)
                {
                    productItem.Name = pItem.Name;
                    productItem.Description = pItem.Description;
                    productItem.Price = pItem.Price;

                    await eshopDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProductController.Select));
                }
            }
            return View(productItem);
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
