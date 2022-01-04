using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hudek.Eshop.Web.Controllers;
using Hudek.Eshop.Web.Models.ApplicationServices.Abstraction;
using Hudek.Eshop.Web.Models.Database;
using Hudek.Eshop.Web.Models.Entity;
using Hudek.Eshop.Web.Models.Identity;

namespace Hudek.Eshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerCartItemsController : Controller
    {
        const string totalPriceString = "TotalPrice";
        const string cartItemsString = "CartItems";


        ISecurityApplicationService iSecure;
        EshopDbContext EshopDbContext;
        public CustomerCartItemsController(ISecurityApplicationService iSecure, EshopDbContext eshopDBContext)
        {
            this.iSecure = iSecure;
            EshopDbContext = eshopDBContext;
        }


        [HttpPost]
        public double AddCartItemsToSession(int? productId)
        {
            double totalPrice = 0;
            if (HttpContext.Session.IsAvailable)
            {
                totalPrice = HttpContext.Session.GetDouble(totalPriceString).GetValueOrDefault();
            }


            ProductItem product = EshopDbContext.ProductItems.Where(prod => prod.ID == productId).FirstOrDefault();

            if (product != null)
            {
                CartItem cartItem = new CartItem()
                {
                    ProductID = product.ID,
                    Product = product,
                    Amount = 1,
                    Price = product.Price   //zde pozor na datový typ -> pokud máte Price v obou případech double nebo decimal, tak je to OK. Mě se bohužel povedlo mít to jednou jako decimal a jednou jako double. Nejlepší je datový typ změnit v databázi/třídě, tak to prosím udělejte.
                };

                if (HttpContext.Session.IsAvailable)
                {

                    List<CartItem> cartItems = HttpContext.Session.GetObject<List<CartItem>>(cartItemsString);
                    CartItem cartItemInSession = null;
                    if (cartItems != null)
                        cartItemInSession = cartItems.Find(oi => oi.ProductID == cartItem.ProductID);
                    else
                        cartItems = new List<CartItem>();


                    if (cartItemInSession != null)
                    {
                        ++cartItemInSession.Amount;
                        cartItemInSession.Price += cartItem.Product.Price;   //zde pozor na datový typ -> pokud máte Price v obou případech double nebo decimal, tak je to OK. Mě se bohužel povedlo mít to jednou jako decimal a jednou jako double. Nejlepší je datový typ změnit v databázi/třídě, tak to prosím udělejte.
                    }
                    else
                    {
                        cartItems.Add(cartItem);
                    }


                    HttpContext.Session.SetObject(cartItemsString, cartItems);

                    totalPrice += cartItem.Product.Price;
                    HttpContext.Session.SetDouble(totalPriceString, totalPrice);
                }
            }

            return totalPrice;
        }


        public async Task<IActionResult> SaveCart()
        {
            if (HttpContext.Session.IsAvailable)
            {


                double totalPrice = 0;
                List<CartItem> orderItems = HttpContext.Session.GetObject<List<CartItem>>(cartItemsString);
                if (orderItems != null)
                {
                    foreach (CartItem orderItem in orderItems)
                    {
                        totalPrice += orderItem.Product.Price * orderItem.Amount;
                        orderItem.Product = null; //zde musime nullovat referenci na produkt, jinak by doslo o pokus jej znovu vlozit do databaze
                    }


                    User currentUser = await iSecure.GetCurrentUser(User);

                    Cart order = new Cart()
                    {
                        CartNumber = currentUser.Id,
                        TotalPrice = totalPrice,
                        CartItems = orderItems,
                    };



                    //We can add just the order; order items will be added automatically.
                    await EshopDbContext.AddAsync(order);
                    await EshopDbContext.SaveChangesAsync();



                    HttpContext.Session.Remove(cartItemsString);
                    HttpContext.Session.Remove(totalPriceString);

                    return RedirectToAction(nameof(CustomerBagController.Index), nameof(CustomerOrdersController).Replace("Controller", ""), new { Area = nameof(Customer) });

                }
            }

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { Area = "" });

        }
    }
}
