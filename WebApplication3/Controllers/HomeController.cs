using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        DBContext reviews = new DBContext();

        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            var Email = User.Identity.Name;
            var userId = reviews.Users.Single(a => a.Email == Email).UsrId;
            ViewBag.userId = userId;
            IEnumerable<Like> Likes = reviews.Likes;
            IEnumerable<Review> review1 = reviews.Reviews;
            ViewBag.Reviews = review1;
            var pagesCount = review1.Count();
            ViewBag.Likes = Likes;
            ViewBag.PagesCount = pagesCount;
            return View();
        }
    }
}