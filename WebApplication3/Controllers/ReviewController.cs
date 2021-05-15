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
    public class ReviewController : Controller
    {
        DBContext games = new DBContext();
        DBContext reviews = new DBContext();


        // GET: Review
        [HttpGet]
        public ActionResult Review()
        {
            IEnumerable<Game> games1 = games.Games;
            SelectList games12 = new SelectList(games1, "GameId", "GameName");
            ViewBag.Games = games1;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(Review review)
        {
            DBContext db = new DBContext();
            string Email = User.Identity.Name;
            review.UserId = db.Users.Single(a => a.Email == Email).UsrId;
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                using (DBContext dc = new DBContext())
                {
                    dc.Reviews.Add(review);
                    dc.SaveChanges();
                    message = "Ваш отзыв будет опубликован";
                    Status = true;
                }
            }
            else message = "Invalid request";
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(review);
        }

        [HttpGet]
        public ActionResult GamesReviews()
        {
            IEnumerable<Review> review1 = reviews.Reviews;
            ViewBag.Reviews = review1;
            IEnumerable<Game> games2 = games.Games;
            ViewBag.Games = games2;
            return View();
        }

        [HttpPost]
        public ActionResult GamesRevievs(Review review)
        {
            DBContext db = new DBContext();
            var gameId = review.GameId;
            ViewBag.GameSelectId = gameId;
            return View();
        }
    }
}