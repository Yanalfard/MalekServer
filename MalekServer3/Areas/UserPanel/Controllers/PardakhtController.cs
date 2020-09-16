﻿using MalekServer3.Models;
using MalekServer3.Models.ObjectClass;
using MalekServer3.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Areas.UserPanel.Controllers
{
    public class PardakhtController : Controller
    {
        Heart heart;
        public PardakhtController()
        {
            heart = new Heart();
        }
        // GET: UserPanel/Pardakht
        public ActionResult Payment(TblClientProductRel tblClientProductRel)
        {
            List<TblClientProductRel> list = new List<TblClientProductRel>();
            List<TblClientProductRel> selectFatorId = heart.TblClientProductRels.ToList();
            int numFactor;
            if (selectFatorId.Count == 0)
            {
                numFactor = 1;
            }
            else
            {
                numFactor = selectFatorId[selectFatorId.Count - 1].FactorId + 1;
            }
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                foreach (var ShopCartItem in cart)
                {
                    var product = heart.TblProducts.SingleOrDefault(i => i.id == ShopCartItem.ProductID);
                    TblClientProductRel AddTbl = new TblClientProductRel()
                    {
                        ProductId = ShopCartItem.ProductID,
                        Count = ShopCartItem.Count,
                        Price = product.Price,
                        Name = product.Name,
                        Date = DateTime.Now.ToString(),
                        IsFinaly = false,
                        FactorId = numFactor,
                        ClientId = tblClientProductRel.ClientId,
                        Discount = tblClientProductRel.Discount,
                        SumFactor= tblClientProductRel.SumFactor,
                        DiscountId= tblClientProductRel.DiscountId
                    };
                    heart.TblClientProductRels.Add(AddTbl);
                    heart.SaveChanges();
                }
                cart.Clear();
            }
            
            System.Net.ServicePointManager.Expect100Continue = false;
            ZarinPal2.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal2.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;

            int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", tblClientProductRel.SumFactor, "ملک سرور", "Iman@Madaeny.com", "09124140939", "https://localhost:44316/UserPanel/Pardakht/Verify/" + numFactor, out Authority);
            if (Status == 100)
            {
                // Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                ViewBag.Error = "Error : " + Status;
            }
            return View();
        }

        public ActionResult Verify(int id)
        {
            TblClientProductRel order = heart.TblClientProductRels.Where(i => i.FactorId == id).ToList()[0];

            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    int Amount = order.SumFactor;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    ZarinPal2.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal2.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE", Request.QueryString["Authority"].ToString(), Amount, out RefID);

                    if (Status == 100)
                    {
                        order.IsFinaly = true;
                        heart.SaveChanges();
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                        // Response.Write("Success!! RefId: " + RefID);
                    }
                    else
                    {
                        ViewBag.Status = Status;
                    }

                }
                else
                {
                    Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                }
            }
            else
            {
                Response.Write("Invalid Input");
            }
            return View();
        }
    }
}