using MalekServer3.Models.ObjectClass;
using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace MalekServer3.Areas.UserPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Heart heart;
        public HomeController()
        {
            heart = new Heart();
        }
        // GET: UserPanel/Home
        public ActionResult Index()
        {
            try
            {

                if (User.Identity.Name == null)
                {
                    throw new NullReferenceException();
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //int userId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1]);
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                string userName = User.Identity.Name.Split('|')[0];
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TblClient client = heart.TblClients.SingleOrDefault(i => i.id == id);
                UpdateRegisterViewModle selectRegisterById = new UpdateRegisterViewModle
                {
                    id = client.id,
                    Name = client.Name,
                    IdentificationNo = client.IdentificationNo,
                    Username = client.Username,
                    TellNo = client.TellNo,

                };
                if (client == null)
                {
                    throw new NullReferenceException();
                    //return HttpNotFound();
                }
                return View(selectRegisterById);
            }
            catch (Exception e)
            {
                throw e;
                //return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UpdateRegisterViewModle register)
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                register.Address = register.Email;
                register.Auth = register.Email;
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(register.OldPassword, "MD5");
                TblClient client = heart.TblClients.SingleOrDefault(i => i.id == register.id);
                if (client.Password != hashPassword)
                {
                    ModelState.AddModelError("OldPassword", "رمز قدیمی اشتباه است");
                    return View(register);
                }
                // go ahead and write code to validate username password against database
                var TestEmailAndUser = heart.TblClients.Where(i => i.id != register.id);
                foreach (var item in TestEmailAndUser)
                {
                    if (item.Username == register.Username)
                    {
                        ModelState.AddModelError("Username", "نام کاربری تکراریست");
                        return View(register);
                    }
                    if (item.Email == register.Email)
                    {
                        ModelState.AddModelError("Username", "ایمیل تکراریست");
                        return View(register);
                    }
                    if (item.IdentificationNo == register.IdentificationNo)
                    {
                        ModelState.AddModelError("IdentificationNo", "کد ملی تکراریست");
                        return View(register);
                    }
                    if (item.TellNo == register.TellNo)
                    {
                        ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                        return View(register);
                    }
                }
                var tblClient = heart.TblClients.SingleOrDefault(i => i.id == register.id);
                tblClient.IdentificationNo = register.IdentificationNo.Trim().ToLower();
                tblClient.Name = register.Name.Trim().ToLower();
                tblClient.TellNo = register.TellNo.Trim().ToLower();
                tblClient.Username = register.Username.Trim().ToLower();
                tblClient.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5");
                tblClient.Address = register.Address;
                tblClient.Email = register.Email;
                heart.SaveChanges();
                return Redirect("Index?recovery=true");
            }
            catch
            {
                return View(register);
            }

        }


        public ActionResult Cart()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<OcProduct> list = new List<OcProduct>();
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                    foreach (var ShopCartItem in cart)
                    {
                        var product = heart.TblProducts.SingleOrDefault(i => i.id == ShopCartItem.ProductID);
                        TblCatagory c = heart.TblCatagories.SingleOrDefault(i => i.id == product.CatagoryId);
                        List<TblImage> imagesForServeice = new List<TblImage>();
                        List<TblProductImageRel> relsForImages = heart.TblProductImageRels.Where(i => i.ProductId == product.id).ToList();
                        foreach (var j in relsForImages)
                            imagesForServeice.Add(heart.TblImages.SingleOrDefault(i => i.id == j.ImageId));

                        list.Add(new OcProduct()
                        {
                            id = ShopCartItem.ProductID,
                            Count = ShopCartItem.Count,
                            Price = product.Price,
                            Name = product.Name,
                            Sum = ShopCartItem.Count * product.Price,
                            Catagory = c,
                            images = imagesForServeice
                        });
                    }
                }
                return View(list);
            }
            catch (Exception e)
            {
                throw e;
                //return HttpNotFound();
            }

        }
        public ActionResult Checkout()
        {

            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<OcProduct> list = new List<OcProduct>();
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                    foreach (var ShopCartItem in cart)
                    {
                        var product = heart.TblProducts.SingleOrDefault(i => i.id == ShopCartItem.ProductID);
                        TblCatagory c = heart.TblCatagories.SingleOrDefault(i => i.id == product.CatagoryId);
                        List<TblImage> imagesForServeice = new List<TblImage>();
                        List<TblProductImageRel> relsForImages = heart.TblProductImageRels.Where(i => i.ProductId == product.id).ToList();
                        foreach (var j in relsForImages)
                            imagesForServeice.Add(heart.TblImages.SingleOrDefault(i => i.id == j.ImageId));

                        list.Add(new OcProduct()
                        {
                            id = ShopCartItem.ProductID,
                            Count = ShopCartItem.Count,
                            Price = product.Price,
                            Name = product.Name,
                            Sum = ShopCartItem.Count * product.Price,
                            Catagory = c,
                            images = imagesForServeice
                        });
                    }
                }
                return View(list);
            }
            catch (Exception e)
            {
                throw e;
                //return HttpNotFound();

            }
        }
        public ActionResult History()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.id == id).ToList();
                List<ShowProduct> showProduct = new List<ShowProduct>();
                foreach (TblClientProductRel i in rels)
                {
                    OcProduct product = new OcProduct(heart.TblProducts.SingleOrDefault(j => j.id == i.id));
                    TblCatagory cat = heart.TblCatagories.SingleOrDefault(j => j.id == product.CatagoryId);

                    ShowProduct db = new ShowProduct()
                    {
                        AfterDiscount = long.Parse(Math.Floor(product.Discount == 0 ? product.Price : product.AfterDiscount).ToString()),
                        cat = cat.Name,
                        product = product.Name,
                        Count = i.Count,
                        id = i.id,
                        Date = i.Date,
                        TotalAfterDiscount = long.Parse(Math.Floor((product.Discount == 0 ? product.Price : product.AfterDiscount) * i.Count).ToString())

                    };
                    showProduct.Add(db);
                }

                return View(showProduct);
            }
            catch (Exception e)
            {
                throw e;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }
        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Order(TblOrder order)
        {
            try
            {
                TblOrder addOrder = heart.TblOrders.Add(order);
                heart.SaveChanges();
                return Json(new { success = true, responseText = "سفارش ثبت شد به زودی با شما تماس حاصل میشود" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ثبت سفارش لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CheckDiscount(string name)
        {
            List<TblDiscount> discout = heart.TblDiscounts.Where(i => i.Name == name && i.Count != 0).ToList();
            if (discout.Count!=0)
            {
                return Json(new { success = true, responseText = discout[0].Discount }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "کد تخفیف موجود نیست" }, JsonRequestBehavior.AllowGet);
        }
    }
}