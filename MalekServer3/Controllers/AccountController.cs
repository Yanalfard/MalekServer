using Antlr.Runtime.Misc;
using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using MalekServer3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MalekServer3.Utilities;

namespace MalekServer3.Controllers
{
    public class AccountController : Controller
    {
        Heart heart;
        public AccountController()
        {
            heart = new Heart();
        }
        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModle register, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!MethodRepo.CheckRechapcha(form))
                    {
                        ViewBag.Message = "لطفا گزینه من ربات نیستم را تکمیل کنید";
                        return View(register);
                    }
                    // go ahead and write code to validate username password against database
                    if (!heart.TblClients.Any(u => u.Email == register.Email.Trim().ToLower()))
                    {
                        if (heart.TblClients.Any(u => u.TellNo == register.TellNo))
                        {
                            ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                            return View(register);
                        }
                        if (heart.TblClients.Any(u => u.Username == register.Username.Trim().ToLower()))
                        {
                           ModelState.AddModelError("Username", "نام کاربری تکراریست");
                            return View(register);
                        }
                        if (heart.TblClients.Any(u => u.TellNo == register.TellNo))
                        {
                            ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                            return View(register);
                        }
                        if (heart.TblClients.Any(u => u.IdentificationNo == register.IdentificationNo))
                        {
                            ModelState.AddModelError("IdentificationNo", "کد ملی تکراریست");
                            return View(register);
                        }
                        TblClient tblClient = new TblClient()
                        {
                            Email = register.Email.Trim().ToLower(),
                            Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                            Auth = Guid.NewGuid().ToString(),
                            IsActive = false,
                            IdentificationNo = register.IdentificationNo.Trim().ToLower(),
                            Name = register.Name.Trim().ToLower(),
                            TellNo = register.TellNo.Trim().ToLower(),
                            Username = register.Username.Trim().ToLower(),
                            Address = register.Address,
                            
                        };
                        heart.TblClients.Add(tblClient);

                        //Send Active Email
                        string body = PartialToStringClass.RenderPartialView("ManageEmails", "ActiviationEmail", tblClient);
                        SendEmail.Send(tblClient.Email, "ایمیل فعالسازی", body);
                        //End Send Active Email
                        heart.SaveChanges();

                        return View("SuccessRegister", tblClient);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                    }
                    //return Redirect("Login");
                }
                return View(register);
            }
            catch
            {
                return View(register);

            }

        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();

        }
        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel login, FormCollection form, string ReturnUrl = "/")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!MethodRepo.CheckRechapcha(form))
                    {
                        ViewBag.Message = "لطفا گزینه من ربات نیستم را تکمیل کنید";
                        return View(login);
                    }
                    string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                    var user = heart.TblClients.SingleOrDefault(u => u.Email == login.Email && u.Password == hashPassword);
                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            if (user.id == 3)
                            {
                                FormsAuthentication.SetAuthCookie(user.Username + "|" + user.id, login.RememberMe);
                                return Redirect("/Admin/Home/Index");
                            }
                            else
                            {
                                FormsAuthentication.SetAuthCookie(user.Username + "|" + user.id, login.RememberMe);
                                return Redirect("/UserPanel/Home/Index");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد");
                    }
                }
                return View(login);
            }
            catch
            {
                return View(login);
            }
        }


        [Route("LogOff")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
        public ActionResult LogPage(int id)
        {
            try
            {
                if (id != 3)
                {
                    return Redirect("/UserPanel/Home/Index");
                }
                else if (id == 3)
                {
                    return Redirect("/Admin/Home/Index");
                }
                return HttpNotFound();
            }
            catch
            {
                return HttpNotFound();

            }

        }
        public ActionResult LogCart(int id)
        {
            try
            {
                if (id != 3)
                {
                    return Redirect("/UserPanel/Home/Cart");
                }
                else if (id == 3)
                {
                    return Redirect("/Admin/Home/Index");
                }
                return HttpNotFound();
            }
            catch
            {
                return HttpNotFound();
            }

        }


        public ActionResult ActiveUser(string id)
        {
            var user = heart.TblClients.SingleOrDefault(u => u.Auth == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = true;
            user.Auth = Guid.NewGuid().ToString();
            heart.SaveChanges();
            ViewBag.UserName = user.Username;
            return View();
        }
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult SuccessRegister()
        {
            return View();
        } 
        public ActionResult SuccesForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (ModelState.IsValid)
            {
                var user = heart.TblClients.SingleOrDefault(u => u.Email == forgot.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        string bodyEmail = PartialToStringClass.RenderPartialView("ManageEmails", "RecoveryPassword", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                        return View("SuccesForgotPassword", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری یافت نشد");
                }
            }
            return View();
        }

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoveryPassword(string id, RecoveryPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = heart.TblClients.SingleOrDefault(u => u.Auth == id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                user.Auth = Guid.NewGuid().ToString();
                heart.SaveChanges();
                return Redirect("/Login?recovery=true");
            }
            return View();
        }

    }
}