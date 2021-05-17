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
            review.UserId = db.Users.Single(a => a.Email == Email).UsrId;
            bool Status = false;
            string message = "";
            if (db.Reviews.Any(a => a.UserId == review.UserId && a.GameId == review.GameId))
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
            var review1 = reviews.Reviews.Where(a => a.GameId == review.GameId).ToList();
            //string sqlCom = "select UserName from Users, Reviews where UsrId = UserId";
            var UserId = db.Users.Take(3);
            //SelectList Range = new SelectList(UserId, "UserId", "UserName");
            ViewBag.Range = UserId;
            ViewBag.Reviews = review1;
            IEnumerable<Game> games2 = games.Games;
            ViewBag.Games = games2;
            ViewBag.Message = message;
            ViewBag.Status = Status;

            IEnumerable<User> user = users.Users;
            ViewBag.Users = user;

            var sel1 = new SelectList(review1, "UserId", "UserId");
            var sel2 = new SelectList(user, "UsrId", "UserName");


            var result = sel1.Join(sel2, s1 => s1.Value, s2 => s2.Value, (s1, s2) => new {Text = s2.Text, Value = s1.Value}).ToList();
            var resultNames = review1.AsQueryable().Join(result, s1 => s1.UserId.ToString(), s2 => s2.Value, (s1, s2) => new { UserId = s2.Text, GameId = s1.GameId, ReviewScore = s1.ReviewScore, ReviewId = s1.ReviewId, ReviewText = s1.ReviewText});
            ViewBag.Users = resultNames;

            return View(review);
        }
    }
}