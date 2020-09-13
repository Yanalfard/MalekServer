using MalekServer3.Models.ObjectClass;
using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class ProductController : Controller
    {
        Heart heart;
        public ProductController()
        {
            heart = new Heart();

        }
        // GET: Product
        public ActionResult NewProduct()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.Take(10).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return PartialView(newProducts);

            }
            catch
            {
                return HttpNotFound();
            }
        }
        public ActionResult New()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.Take(40).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return View(newProducts);
            }
            catch
            {
                return HttpNotFound();
            }

        }
        public ActionResult Suggestions()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.Where(i=>i.IsSuggested).Take(40).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return View(newProducts);

            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}