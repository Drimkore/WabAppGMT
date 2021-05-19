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
    class ReviewShablon
    {
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string ReviewScore { get; set; }
        public string ReviewId { get; set; }
        public string ReviewText { get; set; }

    }
    public class ReviewController : Controller
    {
        DBContext games = new DBContext();
        DBContext reviews = new DBContext();
        DBContext users = new DBContext();


        // GET: Review
        [HttpGet]
        public ActionResult Review()
        {
            IEnumerable<Game> games1 = games.Games;
            List<int> Scores = new List<int>();
            for(int i = 1; i <=10; i++)
            Scores.Add(i);
            ViewBag.scores = Scores;
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
            review.UsrId = db.Users.Single(a => a.Email == Email).UsrId;
            review.UserName = db.Users.Single(a => a.Email == Email).Username;
            bool Status = false;
            string message = "";
            if (db.Reviews.Any(a => a.UsrId == review.UsrId && a.GameId == review.GameId))
            {
                message = "Повторный отзыв (Не Success)";
                Status = true;
                ViewBag.Message = message;
                ViewBag.Status = Status;
                return View(review);
            }    
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
            IEnumerable<User> user = users.Users;
            ViewBag.Users = user;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GamesReviews(Review review)
        {
            DBContext db = new DBContext();
            bool Status = true;
            string message = "Отзывы";
            ViewBag.Message = message;
            ViewBag.Status = Status;
            var review1 = reviews.Reviews.Where(a => a.GameId == review.GameId).ToList();
            ViewBag.Reviews = review1;
            IEnumerable<Game> games2 = games.Games;
            ViewBag.Games = games2;
            return View(review);
        }
    }
}