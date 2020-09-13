using MalekServer3.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class ShopCartController : Controller
    {
        // GET: ShopCart
        public ActionResult Index()
        {
            return View();
        }
        public int AddToCart(int id)
        {
            List<ShopCartItem> cart = new List<ShopCartItem>();
            if (Session["ShopCart"] != null)
            {
                cart = Session["ShopCart"] as List<ShopCartItem>;
            }
            if (cart.Any(p => p.ProductID == id))
            {
                int index = cart.FindIndex(p => p.ProductID == id);
                cart[index].Count += 1;
            }
            else
            {
                cart.Add(new ShopCartItem()
                {
                    ProductID = id,
                    Count = 1
                });
            }
            Session["ShopCart"] = cart;
            return cart.Sum(p=>p.Count);
        }
        public int ShopCartCount()
        {
            int count = 0;

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                count = cart.Sum(p => p.Count);
            }

            return count;
        }
        public ActionResult RemoveFromCart(int id)
        {
            List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
            int index = cart.FindIndex(p => p.ProductID == id);
            cart.RemoveAt(index);
            Session["ShopCart"] = cart;
            return RedirectToAction("Cart", "Home", new { area = "UserPanel" });
        }
    }
}