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
    public class BlogController : Controller
    {
        Heart heart;


        public BlogController()
        {
            heart = new Heart();
        }
        // GET: Blog
        public ActionResult AllBlog()
        {
            try
            {
                List<TblBlog> tblBlog = heart.TblBlogs.OrderByDescending(i=>i.id).Take(40).ToList();
                return View(tblBlog);
            }
            catch 
            {
                return HttpNotFound();
            }
           
        }
    }
}