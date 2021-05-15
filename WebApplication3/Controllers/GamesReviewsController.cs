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
    public class GamesReviewsController : Controller
    {
        DBContext games = new DBContext();
        // GET: GamesReviews
        [HttpGet]
        public ActionResult GamesReviews()
        {
            IEnumerable<Game> games1 = games.Games;
            ViewBag.Games = games1;
            return View();
        }
    }
}