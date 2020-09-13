using MalekServer3.Models;
using MalekServer3.Models.ObjectClass;
using MalekServer3.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Data;
using InsertShowImage;
using KooyWebApp_MVC.Classes;
using System.Web.Security;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Heart heart;

        public HomeController()
        {
            heart = new Heart();
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            try
            {

                if (User.Identity.Name == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //int userId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1]);
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                string userName = User.Identity.Name.Split('|')[0];
                if (id != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TblClient client = heart.TblClients.SingleOrDefault(i => i.id == id);
                UpdateRegisterViewModle selectRegisterById = new UpdateRegisterViewModle
                {
                    id = client.id,
                    Name = client.Name,
                    IdentificationNo = client.IdentificationNo,
                    Username = client.Username,
                    TellNo = client.TellNo,
                    Email = client.Email
                };
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(selectRegisterById);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Index(UpdateRegisterViewModle register)
        {
            try
            {
                register.Address = register.Email;
                register.Auth = register.Email;
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
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
        public ActionResult BlogAdder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BlogAdder(TblBlog tblBlog)
        {
            try
            {
                tblBlog.LikeCount = 0;
                TblBlog addBlog = heart.TblBlogs.Add(tblBlog);
                heart.SaveChanges();
                if (addBlog.id == -1)
                {
                    return Json(new { success = false, responseText = "خطا در ثبت مقاله لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, responseText = "مقاله ثبت شد" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ثبت مقاله لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Blogs()
        {
            List<TblBlog> tblBlog = heart.TblBlogs.ToList();
            return View(tblBlog.OrderByDescending(i => i.id));
        }
        public ActionResult DeleteBlog(int id)
        {
            heart.TblBlogs.Remove(heart.TblBlogs.SingleOrDefault(i => i.id == id));
            heart.SaveChanges();
            return RedirectToAction("Blogs");
        }
        public ActionResult EditBlogs(int id)
        {
            TblBlog selectBlogById = heart.TblBlogs.SingleOrDefault(i => i.id == id);
            return View(selectBlogById);
        }
        [HttpPost]
        public ActionResult EditBlogs(TblBlog tblBlog)
        {
            try
            {
                heart.Entry(tblBlog).State = EntityState.Modified;
                heart.SaveChanges();
                return Json(new { success = true, responseText = " ویرایش مقاله ثبت شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ویرایش مقاله لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Comments()
        {
            List<TblComment> allComments = heart.TblComments.OrderByDescending(i => i.id).ToList();
            return View(allComments);
        }
        public ActionResult CommentValidator()
        {
            List<TblComment> validComments = heart.TblComments.Where(i => !i.IsValid).OrderByDescending(i => i.id).ToList();
            return View(validComments);
        }
        public ActionResult ProductAdder()
        {
            List<TblCatagory> catagory = heart.TblCatagories.ToList();
            return View(catagory);
        }
        public ActionResult EditProduct(int? id)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TblProduct products = heart.TblProducts.SingleOrDefault(i => i.id == id);
                string AllKeys = "";
                List<TblKeyword> keys = new List<TblKeyword>();
                foreach (TblProductKeywordRel j in heart.TblProductKeywordRels.Where(i => i.ProductId == products.id).ToList())
                {
                    keys.Add(heart.TblKeywords.SingleOrDefault(i => i.id == j.KeywordId));
                }
                foreach (var item in keys)
                {
                    AllKeys += item.Name + "،";
                }
                if (AllKeys != "")
                {
                    AllKeys = AllKeys.Remove(AllKeys.Length - 1);
                }
                TblProduct selectedProduct = new TblProduct()
                {
                    id = products.id,
                    Name = products.Name,
                    DateSubmited = products.DateSubmited,
                    Raiting = products.Raiting,
                    Price = products.Price,
                    DescriptionHtml = products.DescriptionHtml,
                    CatagoryId = products.CatagoryId,
                    Count = products.Count,
                    Discount = products.Discount,
                    IsSuggested = products.IsSuggested,
                    IsSlide = false
                };
                OcProduct selectOcProductsById = new OcProduct(selectedProduct);

                return View(selectOcProductsById);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

        }
        [HttpPost]
        public ActionResult EditProduct(TblProduct tblProduct, string keywords, string SelectCatagory, string SelectZirCatagory)
        {
            try
            {
                TblProduct UpdateProduct = new TblProduct();
                UpdateProduct.id = tblProduct.id;
                UpdateProduct.Name = tblProduct.Name;
                UpdateProduct.CatagoryId = heart.TblCatagories.SingleOrDefault(i => i.Name == SelectZirCatagory).id;
                UpdateProduct.DescriptionHtml = tblProduct.DescriptionHtml;
                UpdateProduct.Price = tblProduct.Price;
                UpdateProduct.IsSlide = false;
                UpdateProduct.Discount = tblProduct.Discount;
                UpdateProduct.IsSuggested = tblProduct.IsSuggested;
                UpdateProduct.DateSubmited = tblProduct.DateSubmited;
                UpdateProduct.Raiting = tblProduct.Raiting;

                if (keywords[keywords.Length - 1] == '،')
                {
                    keywords = keywords.Remove(keywords.Length - 1);
                }
                List<TblKeyword> selectKeywords = new List<TblKeyword>();
                List<TblProductKeywordRel> relsForKeys = heart.TblProductKeywordRels.Where(i => i.ProductId == tblProduct.id).ToList();
                foreach (var j in relsForKeys)
                    selectKeywords.Add(heart.TblKeywords.SingleOrDefault(i => i.id == j.KeywordId));

                ///////delte keywords
                foreach (TblKeyword item in selectKeywords)
                {
                    heart.TblKeywords.Remove(item);
                }

                ///////add keywords
                List<TblKeyword> listOfKeywords = new List<TblKeyword>();
                foreach (var item in keywords.Split('،'))
                {
                    TblKeyword keywordsName = new TblKeyword
                    {
                        Name = item
                    };
                    TblKeyword keywordId = heart.TblKeywords.Add(keywordsName);
                    listOfKeywords.Add(keywordId);
                }
                heart.SaveChanges();

                foreach (TblKeyword i in listOfKeywords)
                {
                    if (i != null)
                    {
                        heart.TblProductKeywordRels.Add(new TblProductKeywordRel() { ProductId = tblProduct.id, KeywordId = i.id });
                    }
                }
                heart.Entry(UpdateProduct).State = EntityState.Modified;
                heart.SaveChanges();

                return Json(new { success = true, responseText = "ویرایش شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ویرایش اطلاعات لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);

            }



        }
        public ActionResult PromocodeAdder()
        {
            List<TblDiscount> selectAlldiscount = heart.TblDiscounts.OrderByDescending(i => i.id).ToList();
            return View(selectAlldiscount);
        }
        public ActionResult DeleteDiscount(int id)
        {
            try
            {
                heart.TblDiscounts.Remove(heart.TblDiscounts.SingleOrDefault(i => i.id == id));
                heart.SaveChanges();


                return Json(new { success = true, responseText = " حذف شد" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در حذف  لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddDiscount(string Name, int Discount, int Count)
        {
            try
            {
                TblDiscount AddDis = new TblDiscount()
                {
                    Count = Count,
                    Discount = Discount,
                    Name = Name
                };
                TblDiscount add = heart.TblDiscounts.Add(AddDis);
                heart.SaveChanges();

                return Json(new { success = true, responseText = "درصد اضافه شد" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در اضافه کردن درصد لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Clients()
        {
            List<TblClient> selectAllclient = heart.TblClients.Where(i => i.id != 3).OrderByDescending(j => j.id).ToList();
            return View(selectAllclient);
        }
        public ActionResult DeleteClient(int id, string returnUrl)
        {
            heart.TblClients.Remove(heart.TblClients.SingleOrDefault(i => i.id == id));
            heart.SaveChanges();
            return RedirectToAction("Clients");
        }

        public ActionResult DeleteComment(int id)
        {
            heart.TblComments.Remove(heart.TblComments.SingleOrDefault(i => i.id == id));
            heart.SaveChanges();
            return RedirectToAction("CommentValidator");
        }
        public ActionResult ShowHideComment(int id)
        {
            TblComment oldComment = heart.TblComments.SingleOrDefault(i => i.id == id);
            oldComment.IsValid = !oldComment.IsValid;
            heart.SaveChanges();
            return RedirectToAction("CommentValidator");
        }
        public ActionResult Products()
        {
            List<TblProduct> selectAllProducts = heart.TblProducts.OrderByDescending(i => i.id).ToList();
            return View(selectAllProducts);
        }
        [HttpPost]
        public ActionResult AddCatagory(string CatagoryName)
        {
            try
            {
                TblCatagory tblCatagory = heart.TblCatagories.SingleOrDefault(i => i.Name == CatagoryName);
                if (tblCatagory != null)
                {
                    return Json(new { success = false, responseText = "نام دسته تکراریست" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    heart.TblCatagories.Add(new TblCatagory() { Name = CatagoryName, CatagoryId = null, IsOnFirstPage = false });
                    heart.SaveChanges();
                    return Json(new { success = true, responseText = "ثبت شد" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = "نام دسته تکراریست" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult UpdateCatagory(TblCatagory tblCatagory1)
        {
            try
            {
                
                List<TblCatagory> TestId = heart.TblCatagories.Where(i => i.id != tblCatagory1.id).ToList();
                foreach (var item in TestId)
                {
                    if (item.Name == tblCatagory1.Name)
                    {
                        return Json(new { success = false, responseText = "نام دسته تکراریست" }, JsonRequestBehavior.AllowGet);
                    }
                }

                TblCatagory payment = new TblCatagory();
                payment = heart.TblCatagories.Find(tblCatagory1.id);
                payment.id = tblCatagory1.id;
                payment.Name = tblCatagory1.Name;
                payment.IsOnFirstPage = false;
                heart.Entry(payment).State = EntityState.Modified;
                heart.SaveChanges();
                return Json(new { success = true, responseText = "ثبت شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "نام دسته تکراریست" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult AddZirCatagory(string ZirCatagoryName, string NameCatagoryAdd, bool IsOnFirstPage)
        {
            try
            {
                TblCatagory tblCatagory = heart.TblCatagories.SingleOrDefault(i => i.Name == ZirCatagoryName);
                int idcati = Convert.ToInt32(NameCatagoryAdd);
                TblCatagory searchIdCatagory = heart.TblCatagories.SingleOrDefault(i => i.id == idcati);
                if (tblCatagory != null)
                {
                    return Json(new { success = false, responseText = "زیر  دسته تکراریست" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    heart.TblCatagories.Add(new TblCatagory() { Name = ZirCatagoryName, CatagoryId = searchIdCatagory.id, IsOnFirstPage = IsOnFirstPage });
                    heart.SaveChanges();
                    return Json(new { success = true, responseText = "ثبت شد" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = " دسته مورد نظر پیدا نشد" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UpdateZirCatagory(TblCatagory tblCatagory1)
        {
            try
            {
                List<TblCatagory> TestId = heart.TblCatagories.Where(i => i.id != tblCatagory1.id).ToList();
                foreach (var item in TestId)
                {
                    if (item.Name == tblCatagory1.Name)
                    {
                        return Json(new { success = false, responseText = "نام زیر دسته تکراریست" }, JsonRequestBehavior.AllowGet);
                    }
                }
                TblCatagory payment = new TblCatagory();
                payment = heart.TblCatagories.Find(tblCatagory1.id);
                payment.id = tblCatagory1.id;
                payment.Name = tblCatagory1.Name;
                payment.IsOnFirstPage = tblCatagory1.IsOnFirstPage;
                heart.Entry(payment).State = EntityState.Modified;
                heart.SaveChanges();
                return Json(new { success = true, responseText = "ثبت شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = " زیر دسته مورد نظر پیدا نشد " }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult AddProduct(TblProduct tblProduct, string keywords, string SelectCatagory, string SelectZirCatagory)
        {
            try
            {
                TblProduct add;

                TblProduct addProduct = new TblProduct();
                addProduct.Name = tblProduct.Name;
                addProduct.CatagoryId = heart.TblCatagories.SingleOrDefault(i => i.Name == SelectZirCatagory).id;
                addProduct.DescriptionHtml = tblProduct.DescriptionHtml;
                addProduct.DateSubmited = DateTime.Now.ToString();
                addProduct.Price = tblProduct.Price;
                addProduct.IsSlide = false;
                addProduct.Raiting = 3;
                addProduct.Discount = tblProduct.Discount;
                addProduct.IsSuggested = tblProduct.IsSuggested;
                addProduct.Raiting = 0;
                add = heart.TblProducts.Add(addProduct);
                if (keywords[keywords.Length - 1] == '،')
                {
                    keywords = keywords.Remove(keywords.Length - 1);
                }
                List<TblKeyword> listOfKeywords = new List<TblKeyword>();
                foreach (var item in keywords.Split('،'))
                {
                    TblKeyword keywordsName = new TblKeyword
                    {
                        Name = item
                    };
                    TblKeyword keywordId = heart.TblKeywords.Add(keywordsName);
                    listOfKeywords.Add(keywordId);
                }
                heart.SaveChanges();
                foreach (TblKeyword i in listOfKeywords)
                {
                    if (i != null)
                    {
                        heart.TblProductKeywordRels.Add(new TblProductKeywordRel() { ProductId = add.id, KeywordId = i.id });
                    }
                }

                heart.SaveChanges();
                return Json(new { success = true, responseText = "ثبت شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ویرایش اطلاعات لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                heart.TblProducts.Remove(heart.TblProducts.SingleOrDefault(i => i.id == id));
                heart.SaveChanges();
                return Json(new { success = true, responseText = "حذف شد" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در حذف اطلاعات لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public string ChangeCatagory(string NameCatagory)
        {
            try
            {
                if (NameCatagory == "")
                {
                    return JsonConvert.SerializeObject("Err022");

                }
                TblCatagory catagory = heart.TblCatagories.SingleOrDefault(j => j.Name == NameCatagory);
                List<TblCatagory> searchIdCatagory = heart.TblCatagories.Where(i => i.CatagoryId == catagory.id).ToList();
                return JsonConvert.SerializeObject(searchIdCatagory, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch
            {
                return JsonConvert.SerializeObject("خطا در دریافت اطلاعات");
            }


        }
        [HttpPost]
        public string ChangeMotherCatagory(string NamemotherCatagory)
        {
            try
            {
                TblCatagory search = heart.TblCatagories.SingleOrDefault(i => i.Name == NamemotherCatagory);
                TblCatagory searchIdCatagory = heart.TblCatagories.SingleOrDefault(i => i.id == search.id);
                return JsonConvert.SerializeObject(searchIdCatagory.Name, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch
            {
                return JsonConvert.SerializeObject("خطا در دریافت اطلاعات");

            }


        }



        public ActionResult Gallery(int id)
        {
            List<TblProductImageRel> rels = heart.TblProductImageRels.Where(i => i.ProductId == id).ToList();
            List<TblImage> images = new List<TblImage>();
            foreach (var i in rels)
            {
                images.Add(heart.TblImages.SingleOrDefault(j => j.id == i.ImageId));
            }
            ViewBag.Galleries = images.ToList();
            return View(new Product_Galleries()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult Gallery(Product_Galleries galleries, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null && imgUp.IsImage())
                {
                    galleries.Image = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Resources/Images/" + galleries.Image));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Resources/Images/" + galleries.Image),
                        Server.MapPath("/Resources/Images/Thumb/" + galleries.Image));
                    TblImage imageToAdd = new TblImage { Image = galleries.Image };
                    imageToAdd = heart.TblImages.Add(imageToAdd);
                    heart.TblProductImageRels.Add(new TblProductImageRel { ImageId = imageToAdd.id, ProductId = galleries.ProductID });
                    heart.SaveChanges();
                }
            }

            return RedirectToAction("Gallery", new { id = galleries.ProductID });
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = heart.TblImages.Find(id);
            int productId = heart.TblProductImageRels.Where(i => i.ImageId == id).ToList()[0].ProductId;
            System.IO.File.Delete(Server.MapPath("/Resources/Images/" + gallery.Image));
            System.IO.File.Delete(Server.MapPath("/Resources/Images/Thumb/" + gallery.Image));
            heart.TblImages.Remove(gallery);
            heart.SaveChanges();
            return RedirectToAction("Gallery", new { id = productId });
        }
        public ActionResult Property(int id)
        {
            List<TblProductPropertyRel> rels = heart.TblProductPropertyRels.Where(i => i.ProductId == id).ToList();
            List<TblProperty> property = new List<TblProperty>();
            foreach (var i in rels)
            {
                property.Add(heart.TblProperties.SingleOrDefault(j => j.id == i.PropertyId));
            }
            ViewBag.Property = property.ToList();
            return View(new Product_Property()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult Property(Product_Property propertyies)
        {
            if (propertyies.Description != null || propertyies.Name != null)
            {
                TblProperty propertyToAdd = new TblProperty { Name = propertyies.Name, Description = propertyies.Description };
                propertyToAdd = heart.TblProperties.Add(propertyToAdd);
                heart.TblProductPropertyRels.Add(new TblProductPropertyRel { PropertyId = propertyToAdd.id, ProductId = propertyies.ProductID });
                heart.SaveChanges();
            }
            return RedirectToAction("Property", new { id = propertyies.ProductID });
        }

        public ActionResult DeleteProperty(int id)
        {
            var Property = heart.TblProperties.Find(id);
            int productId = heart.TblProductPropertyRels.Where(i => i.PropertyId == id).ToList()[0].ProductId;
            heart.TblProperties.Remove(Property);
            heart.SaveChanges();
            return RedirectToAction("Property", new { id = productId });
        }

        [HttpPost]
        public ActionResult DeleteCatagory(int id)
        {
            try
            {
                if (heart.TblProducts.Any(u => u.CatagoryId == id))
                {
                    return Json(new { success = false, responseText = " دسته مورد نظر قابل حذف نیست" }, JsonRequestBehavior.AllowGet);
                }
                if (heart.TblCatagories.Any(u => u.CatagoryId == id))
                {
                    return Json(new { success = false, responseText = " دسته مورد نظر قابل حذف نیست" }, JsonRequestBehavior.AllowGet);
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