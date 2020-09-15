using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Areas.UserPanel.Controllers
{
    public class PardakhtController : Controller
    {
        //Heart heart;
        //public PardakhtController()
        //{
        //    heart = new Heart();
        //}
        //// GET: UserPanel/Pardakht
        //public ActionResult Payment(int id)
        //{

        //    var product = heart.TblProducts.Find(id);

        //    tblpr order = new TblOrder();
        //    order.DateTime = DateTime.Now;
        //    order.IsFinaly = false;
        //    order.Sum = product.Price;

        //    heart.TblOrders.Add(order);
        //    heart.SaveChanges();

        //    System.Net.ServicePointManager.Expect100Continue = false;
        //    ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
        //    string Authority;

        //    int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", order.Sum, "تست درگاه زرین پال در تاپ لرن", "Iman@Madaeny.com", "09124140939", "http://localhost:45233/Home/Verify/" + order.OrderID, out Authority);

        //    if (Status == 100)
        //    {
        //        // Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
        //        Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Error : " + Status;
        //    }
        //    return View();
        //}

        //public ActionResult Verify(int id)
        //{
        //    var order = heart.TblOrders.Find(id);


        //    if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
        //    {
        //        if (Request.QueryString["Status"].ToString().Equals("OK"))
        //        {
        //            int Amount = order.Sum;
        //            long RefID;
        //            System.Net.ServicePointManager.Expect100Continue = false;
        //            ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();

        //            int Status = zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE", Request.QueryString["Authority"].ToString(), Amount, out RefID);

        //            if (Status == 100)
        //            {
        //                order.IsFinaly = true;
        //                heart.SaveChanges();
        //                ViewBag.IsSuccess = true;
        //                ViewBag.RefId = RefID;
        //                // Response.Write("Success!! RefId: " + RefID);
        //            }
        //            else
        //            {
        //                ViewBag.Status = Status;
        //            }

        //        }
        //        else
        //        {
        //            Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("Invalid Input");
        //    }
        //    return View();
        //}
    }
}