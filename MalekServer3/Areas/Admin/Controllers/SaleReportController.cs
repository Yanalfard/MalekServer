using MalekServer3.Models;
using MalekServer3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class SaleReportController : Controller
    {
        Heart heart;
        public SaleReportController()
        {
            heart = new Heart();
        }

        // GET: Admin/SaleReport
        public ActionResult SaleReports()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id != 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.ClientId != id).DistinctBy(i => i.FactorId).OrderByDescending(i => i.id).ToList();
                return View(rels);
            }
            catch (Exception e)
            {
                throw e;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult NamyeshFactor(int? id)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.IsFinaly && i.ClientId != idLogin && i.FactorId == id).ToList();
                return View(rels);
            }
            catch (Exception e)
            {
                throw e;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }



    }
}