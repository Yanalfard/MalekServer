using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MalekServer3.Models.ObjectClass;
using MalekServer3.Models.ViewModel;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Security;
using System.Data;
using InsertShowImage;
using KooyWebApp_MVC.Classes;
using System.Web.Security;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class Product_GroupsController : Controller
    {
        Heart heart;

        public Product_GroupsController()
        {
            heart = new Heart();
        }
        // GET: Admin/Product_Groups
        public ActionResult Index()
        {
            int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            if (idLogin != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        public ActionResult ListGroups()
        {

            var product_Groups = heart.TblCatagories.Where(g => g.CatagoryId == null);
            return PartialView(product_Groups.ToList());
        }

        // GET: Admin/Product_Groups/Details/5
        public ActionResult Details(int? id)
        {
            int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            if (idLogin != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCatagory product_Groups = heart.TblCatagories.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            return View(product_Groups);
        }

        // GET: Admin/Product_Groups/Create
        public ActionResult Create()
        {
            return PartialView();

        }
        public ActionResult CreateZirDate(int? id)
        {
            return PartialView(new TblCatagory()
            {
                CatagoryId = id
            });
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCatagory product_Groups = heart.TblCatagories.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }

            return PartialView(product_Groups);
        }
        public ActionResult EditZirCatagory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblCatagory product_Groups = heart.TblCatagories.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }

            return PartialView(product_Groups);
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (heart.TblProducts.Any(u => u.CatagoryId == id))
                {
                    return Json(new { success = true, responseText = " دسته مورد نظر قابل حذف نیست" }, JsonRequestBehavior.AllowGet);
                }
                if (heart.TblCatagories.Any(u => u.CatagoryId == id))
                {
                    return Json(new { success = true, responseText = " دسته مورد نظر قابل حذف نیست" }, JsonRequestBehavior.AllowGet);
                }
                TblCatagory product_Groups = heart.TblCatagories.Find(id);
                heart.TblCatagories.Remove(product_Groups);
                heart.SaveChanges();
                return Json(new { success = true, responseText = "حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = " دسته مورد نظر قابل حذف نیست" }, JsonRequestBehavior.AllowGet);
            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                heart.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}