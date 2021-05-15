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
            IEnumerable<Review> review1 = reviews.Reviews;
            ViewBag.Reviews = review1;
            var pagesCount = review1.Count();
            ViewBag.PagesCount = pagesCount;



            return View();
        }


    }
}