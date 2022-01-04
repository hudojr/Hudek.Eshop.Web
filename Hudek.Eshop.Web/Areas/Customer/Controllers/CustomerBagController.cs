using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hudek.Eshop.Web.Models.ApplicationServices.Abstraction;
using Hudek.Eshop.Web.Models.Database;
using Hudek.Eshop.Web.Models.Entity;
using Hudek.Eshop.Web.Models.Identity;
namespace Hudek.Eshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerBagController : Controller
    {
        private readonly EshopDbContext _context;
        ISecurityApplicationService iSecure;

        const string totalPriceString = "TotalPrice";
        const string cartItemsString = "CartItems";

        public CustomerBagController(EshopDbContext context, ISecurityApplicationService iSecure)
        {
            this.iSecure = iSecure;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = await iSecure.GetCurrentUser(User);
                if (currentUser != null)
                {
                    //var eshopDbContext = _context.CartItem
                    //    .Where(or => or.Order.UserId == currentUser.Id)
                    //    .Include(o => o.Order)
                    //    .Include(o => o.Product);
                    //await eshopDbContext.ToListAsync();

                    if (HttpContext.Session.IsAvailable)
                    {
                        double totalPrice = 0;
                        List<CartItem> orderItems = HttpContext.Session.GetObject<List<CartItem>>(cartItemsString);
                        if (orderItems != null)
                        {
                            foreach (CartItem orderItem in orderItems)
                            {
                                totalPrice += orderItem.Product.Price * orderItem.Amount;
                            }
                            var eshopDbContext2 = orderItems;
                            return View(eshopDbContext2);
                        }
                        return NotFound();
                    }

                }
            }
            return View();
        }

    }
}
