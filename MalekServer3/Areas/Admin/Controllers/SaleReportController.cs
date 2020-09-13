using MalekServer3.Models;
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
            List<TblClientProductRel> clientProduct = heart.TblClientProductRels.OrderByDescending(i=>i.id).ToList();
            return View(clientProduct);
        }
    }
}