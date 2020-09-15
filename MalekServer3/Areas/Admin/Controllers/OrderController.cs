using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        Heart heart;
        public OrderController()
        {
            heart = new Heart();
        }
        // GET: Admin/Order
        public ActionResult Orders()
        {
            int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            if (idLogin != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<TblOrder> ordersProduct = heart.TblOrders.OrderByDescending(i => i.id).ToList();
            return View(ordersProduct);
        }
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
            TblOrder features = heart.TblOrders.Find(id);
            if (features == null)
            {
                return HttpNotFound();
            }
            return View(features);
        }
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                heart.TblOrders.Remove(heart.TblOrders.SingleOrDefault(i => i.id == id));
                heart.SaveChanges();
                return Json(new { success = true, responseText = "حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در حذف آیتم لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}