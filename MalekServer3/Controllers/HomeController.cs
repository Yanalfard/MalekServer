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
    public class HomeController : Controller
    {
        Heart heart;

        public HomeController()
        {
            heart = new Heart();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IsOnFirstPage()
        {
            try
            {
                List<OcCatagory> cats = new List<OcCatagory>();
                List<TblCatagory> AllCatagory = heart.TblCatagories.Where(i => i.IsOnFirstPage).ToList();
                foreach (TblCatagory i in AllCatagory)
                {
                    cats.Add(new OcCatagory(i.id, i.Name, i.CatagoryId, new List<OcProduct>()));
                    for (int k = 0; k < cats.Count; k++)
                    {
                        int ctId = cats[k].id;
                        List<TblProduct> prs = heart.TblProducts.Where(j => j.CatagoryId == ctId).ToList();
                        foreach (TblProduct item in prs)
                        {
                            cats[k].Products.Add(new OcProduct(item));
                        }
                    }
                }

                return PartialView(cats);
            }
            catch (Exception e)
            {
                return HttpNotFound("SelectButtonProducts has not worked");
            }
        }
        public ActionResult ProductHub()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.OrderBy(r => Guid.NewGuid()).Take(10).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return PartialView(newProducts);
            }
            catch
            {
                return HttpNotFound("SelectButtonProducts has not worked");
            }
        }
        public ActionResult ProductHub2()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.OrderBy(r => Guid.NewGuid()).Skip(2).Take(10).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return PartialView(newProducts);
            }
            catch
            {
                return HttpNotFound("SelectButtonProducts has not worked");
            }
        }
        public ActionResult ProductHub3()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.OrderBy(r => Guid.NewGuid()).Skip(4).Take(10).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return PartialView(newProducts);
            }
            catch
            {
                return HttpNotFound("SelectButtonProducts has not worked");
            }
        }
        public ActionResult Product(int id)
        {
            try
            {
                var Product = new OcProduct(heart.TblProducts.SingleOrDefault(i => i.id == id));

                if (Product == null)
                {
                    return HttpNotFound("SelectProductById  has not worked");
                }
                //return View("/Product?Name=" + Product.Name);
                return View(Product);

            }
            catch
            {
                return HttpNotFound("SelectProductById has not worked");
            }
        }
        public ActionResult AddComment(string id, string raiting, string comment, string userId)
        {
            try
            {
                int ConverRating = Convert.ToInt32(raiting);
                ConverRating *= 20;
                int id2 = Convert.ToInt32(id);
                if (id == "" || raiting == "" || comment == "" || userId == "")
                {
                    return Json(new { success = false, responseText = "خطا در ثبت پیام لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
                }
                TblComment addcomment = new TblComment()
                {
                    Body = comment,
                    Date = DateTime.Now.ToShortDateString(),
                    FromId = Convert.ToInt32(userId),
                    IsValid = false,
                };
                var commentId = heart.TblComments.Add(addcomment);
                TblProductCommentRel addCommentId = new TblProductCommentRel
                {
                    CommentId = commentId.id,
                    ProductId = Convert.ToInt32(id)
                };
                TblProduct product = heart.TblProducts.SingleOrDefault(i => i.id == id2);
                int count = heart.TblProductCommentRels.Where(j => j.ProductId == id2).ToList().Count;
                if (count == 0)
                {
                    product.Raiting = ConverRating;
                }
                else
                {
                    product.Raiting = Convert.ToInt32((product.Raiting * (count - 1) + ConverRating) / count);
                }
                TblProductCommentRel productCommentRel = heart.TblProductCommentRels.Add(addCommentId);
                heart.SaveChanges();
                return Json(new { success = true, responseText = "پیام شما ارسال شد" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ثبت پیام لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Blogs()
        {
            try
            {
                List<TblBlog> tblBlog = heart.TblBlogs.Take(5).ToList();
                return PartialView(tblBlog);
            }
            catch
            {
                return HttpNotFound("SelectButtonBlogs has not worked");
            }
        }
        public ActionResult Blog(int id)
        {
            try
            {

                TblBlog selectBlogById = heart.TblBlogs.SingleOrDefault(i => i.id == id);
                return View(selectBlogById);
            }
            catch
            {
                return HttpNotFound("SelectProductById has not worked");
            }
        }
        public ActionResult Suggestion()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.Where(i => i.IsSuggested).Take(10).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return PartialView(newProducts);

            }
            catch
            {
                return HttpNotFound("SelectProductByIsSuggested has not worked");
            }
        }
        public ActionResult Slider()
        {
            try
            {
                List<TblProduct> products = heart.TblProducts.Where(i => i.IsSlide).Take(4).ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return PartialView(newProducts);
            }
            catch
            {
                return HttpNotFound("SelectProductByIsSlide has not worked");
            }

        }
        public ActionResult Ads1()
        {
            try
            {
                List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 1).Take(4).ToList();
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult Ads2()
        {
            try
            {
                List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 2).Take(4).ToList();
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult Ads3()
        {
            try
            {
                List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 3).Take(4).ToList();
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult Ads4()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.Where(i => i.PositionId == 4).ToList()[0];
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult Ads5()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.Where(i => i.PositionId == 5).ToList()[0];
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult Ads6()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.Where(i => i.PositionId == 6).ToList()[0];
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult Search(string q)
        {
            try
            {
                ViewBag.Search = q;
                List<TblProduct> products = heart.TblProducts.ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                //return View(newProducts.Where(p => p.Name.Contains(q) || p.CatagoryId == heart.TblCatagories.SingleOrDefault(i => i.Name == q).id || p.Keyword.Contains(heart.TblKeywords.SingleOrDefault(i => i.Name == q))).Distinct());
                return View(newProducts.Where(p => p.Name.Contains(q) || p.Catagory.Name.Contains(q)).Distinct());

            }
            catch
            {
                return HttpNotFound("SelectAllProducts has not worked");
            }
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Catagorys()
        {
            try
            {
                List<TblCatagory> catagory = heart.TblCatagories.ToList();
                return PartialView(catagory);
            }
            catch
            {
                return HttpNotFound("SelectAllCatagorys has not worked");
            }
        }

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
        [Route("SiteMap")]
        public ActionResult SiteMap()
        {
            return View();
        }
    }
}