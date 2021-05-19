using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication3.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace WebApplication3.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                var ifExist = IsEmailExist(user.Email, user.Username);
                if (ifExist)
                {
                    ModelState.AddModelError("EmailExist", "Выберите другой Email или Username");
                    return View(user);
                }
                user.Password = Crypto.Hash(user.Password);

                using (DBContext dc = new DBContext())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();
                    message = "Registration complete";
                    Status = true;
                }
            }
            else message = "Invalid request";
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (DBContext dc = new DBContext())
            {
                var v = dc.Users.Where(a => a.Email == login.Email).FirstOrDefault();
                if (v != null)  
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Неверные данные";
                    }
                }
                else
                {
                    message = "Неверные данные";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string Email, string Username)
        {
            using (DBContext dc = new DBContext())
            {
                var u = dc.Users.Where(a => a.Username == Username).FirstOrDefault();
                var v = dc.Users.Where(a => a.Email == Email).FirstOrDefault();
                return v != null || u != null;
            }
        }
    }
}