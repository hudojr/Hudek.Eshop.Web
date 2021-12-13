using Hudek.Eshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hudek.Eshop.Web.Models.Database;
using Hudek.Eshop.Web.Models.ViewModels;


namespace Hudek.Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly EshopDbContext eshopDbContext;
        public HomeController(EshopDbContext eshopDb, ILogger<HomeController> logger)
        {
            _logger = logger;
            eshopDbContext = eshopDb;
            
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Načtení Home Index");
            IndexViewModels indexVM = new IndexViewModels();

            indexVM.CarouselItems = eshopDbContext.CarouselItems.ToList();
            indexVM.ProductItems = eshopDbContext.ProductItems.ToList();

            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
