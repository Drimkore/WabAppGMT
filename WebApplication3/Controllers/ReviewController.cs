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
using System.IO;

namespace WebApplication3.Controllers
{
    public class ReviewController : Controller
    {
        DBContext games = new DBContext();
        DBContext reviews = new DBContext();
        DBContext users = new DBContext();


        // GET: Review
        [System.Web.Mvc.HttpGet]
        public ActionResult Review()
        {
            var games1 = games.Games;
            List<int> Scores = new List<int>();
            for (int i = 1; i <= 10; i++)
                Scores.Add(i);
            ViewBag.Scores = Scores;
            ViewBag.Games = games1;
            return View();
        }
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(Review review)
        {
            DBContext db = new DBContext();
            string Email = User.Identity.Name;
            review.Rating = 0;
            review.Username = db.Users.Single(b => b.Email == Email).Username;
            review.UserId = db.Users.Single(a => a.Email == Email).UsrId;
            review.Game = db.Games.Single(a => a.GameId == review.GameId).GameName;
            review.ReviewTime = DateTime.Today;
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

        [System.Web.Mvc.HttpGet]
        public ActionResult GamesReviews()
        {
            IEnumerable<Review> review1 = reviews.Reviews;
            ViewBag.Reviews = review1;
            IEnumerable<Game> games2 = games.Games;
            ViewBag.Games = games2;
            var games3 = games.Games.FirstOrDefault().GameName;
            ViewBag.Names = games3;
            IEnumerable<User> user = users.Users;
            ViewBag.Users = user;
            return View();
        }

        [System.Web.Mvc.HttpPost]
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
        [System.Web.Mvc.HttpPost]
        public ActionResult WordSearch(string word, int choose)
        {
            List<Review> allReviews = new List<Review>();
            if (choose == 1)
            {
                allReviews = reviews.Reviews.Where(a => a.Username.Contains(word)).ToList();
            }
            if (choose == 2)
            {
                allReviews = reviews.Reviews.Where(a => a.ReviewText.Contains(word)).ToList();
            }
            if (choose == 3)
            {
                allReviews = reviews.Reviews.Where(a => a.Game.Contains(word)).ToList();
            }
            if (allReviews.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allReviews);

        
        

        }
        //public string Like(bool mark, int reviewId, int userId)
        //{
        //    var review1 = reviews.Reviews.FirstOrDefault(a => a.ReviewId == reviewId);
        //    var checkReview = reviews.Likes.Any(a => a.UserId.Equals(reviewId));
        //    var checkUser = reviews.Likes.Any(a => a.ReviewId.Equals(userId));
        //    if (checkReview && checkUser)
        //    {
        //        if (!reviews.Likes.Single(a => a.ReviewId.Equals(reviewId)).IsLiked)
        //        {
        //            var changedreview = reviews.Reviews.Single(a => a.ReviewId.Equals(reviewId));
        //            var changeLike = reviews.Likes.Single(a => a.ReviewId.Equals(reviewId));
        //            changedreview.Rating++;
        //            changeLike.IsLiked = true;
        //            reviews.SaveChanges();
        //        }
        //        else
        //        {
        //            var changedreview = reviews.Reviews.Single(a => a.ReviewId.Equals(reviewId));
        //            var changeLike = reviews.Likes.Single(a => a.ReviewId.Equals(reviewId));
        //            changedreview.Rating--;
        //            changeLike.IsLiked = false;
        //            reviews.SaveChanges();
        //        }
        //    }
        //    else
        //    {
        //        if (mark)
        //        {
        //            var changedReview = reviews.Reviews.Single(a => a.ReviewScore.Equals(reviewId));
        //            changedReview.Rating++;
        //            var newLike = new Like();
        //            newLike.IsLiked = true;
        //            newLike.ReviewId = reviewId;
        //            newLike.UserId = userId;
        //            reviews.Likes.Add(newLike);
        //            reviews.SaveChanges();
        //        }
        //    }
        //    return review1.Rating.ToString();
        //}
        [HttpGet]
        public string Like(int id)
        {
            var review1 = reviews.Reviews.FirstOrDefault(a => a.ReviewId == id);
            var reviewId = review1.ReviewId;
            var userId = review1.UserId;
            
            var checkReview = reviews.Likes.Any(a => a.UserId.Equals(reviewId));
            var checkUser = reviews.Likes.Any(a => a.ReviewId.Equals(userId));
            if (checkReview && checkUser)
            {
                if (!reviews.Likes.Single(a => a.ReviewId.Equals(reviewId)).IsLiked)
                {
                    var changedreview = reviews.Reviews.Single(a => a.ReviewId.Equals(reviewId));
                    var changeLike = reviews.Likes.Single(a => a.ReviewId.Equals(reviewId));
                    changedreview.Rating++;
                    changeLike.IsLiked = true;
                    reviews.SaveChanges();
                }
                else
                {
                    var changedreview = reviews.Reviews.Single(a => a.ReviewId.Equals(reviewId));
                    var changeLike = reviews.Likes.Single(a => a.ReviewId.Equals(reviewId));
                    changedreview.Rating--;
                    changeLike.IsLiked = false;
                    reviews.SaveChanges();
                }
            }
            else
            {
                    var changedReview = reviews.Reviews.Single(a => a.ReviewId.Equals(reviewId));
                    changedReview.Rating++;
                    var newLike = new Like();
                    newLike.IsLiked = true;
                    newLike.ReviewId = reviewId;
                    newLike.UserId = userId;
                    reviews.Likes.Add(newLike);
                    reviews.SaveChanges();
            }
            return review1.Rating.ToString();
        }


        [System.Web.Mvc.HttpGet]
        public ActionResult MyReviews()
        {
            List<int> Scores = new List<int>();
            for (int i = 1; i <= 10; i++)
                Scores.Add(i);
            ViewBag.Scores = Scores;
            var Email = User.Identity.Name;
            var Username = reviews.Users.Single(a => a.Email == Email).Username;
            var reviewsMy = reviews.Reviews.Where(a => a.Username.Equals(Username)).ToList();
            ViewBag.MyReviews = reviewsMy;
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult Myreviews( List<Review> review)
        {
            foreach (var l in review)
            {
                var changedreview = reviews.Reviews.Single(a => a.ReviewId.Equals(l.ReviewId));
                if (l.ReviewText != null)
                {
                    changedreview.ReviewScore = l.ReviewScore;
                    changedreview.ReviewText = l.ReviewText;
                    changedreview.ReviewTime = DateTime.Today;
                }
            }
            reviews.SaveChanges();
            List<int> Scores = new List<int>();
            for (int i = 1; i <= 10; i++)
                Scores.Add(i);
            ViewBag.Scores = Scores;
            var Email = User.Identity.Name;
            var Username = reviews.Users.Single(a => a.Email == Email).Username;
            var reviewsMy = reviews.Reviews.Where(a => a.Username.Equals(Username)).ToList();
            ViewBag.MyReviews = reviewsMy;


            return View();

        }
    }
}