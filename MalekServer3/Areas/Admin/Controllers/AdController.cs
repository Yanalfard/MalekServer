using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MalekServer3.Models;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        Heart heart;
        public AdController()
        {
            heart = new Heart();
        }

        // GET: Admin/Ad
        public ActionResult Ads()
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblAd> selectAllAdd = heart.TblAds.OrderByDescending(i => i.id).ToList();
                return View(selectAllAdd);
            }
            catch
            {
                return HttpNotFound();
            }

        }
        public ActionResult AddAd()
        {
            int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            if (idLogin != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddAd(TblAd Add)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TblAd addAd = heart.TblAds.Add(Add);
                heart.SaveChanges();
                if (addAd.id == -1)
                {
                    return Json(new { success = false, responseText = "خطا در ثبت تبلیغ لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, responseText = "تبلیغ ثبت شد" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ثبت تبلیغ لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult DeleteAd(int id)
        {
            heart.TblAds.Remove(heart.TblAds.SingleOrDefault(i => i.id == id));
            heart.SaveChanges();
            return RedirectToAction("Ads");
        }
        public ActionResult EditAd(int id)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TblAd selectAdById = heart.TblAds.SingleOrDefault(i => i.id == id);
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();

            }

        }
        [HttpPost]
        public ActionResult EditAd(TblAd update)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //bool updateAd = _adService.UpdateAd(update, update.id);
                heart.Entry(update).State = EntityState.Modified;
                heart.SaveChanges();
               
                return Json(new { success = true, responseText = " ویرایش تبلیغ ثبت شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ویرایش تبلیغ لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}