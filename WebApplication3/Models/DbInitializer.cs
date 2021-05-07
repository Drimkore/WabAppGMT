using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public class DbInitializer12 : DropCreateDatabaseAlways<DBContext>
    {
        protected override void Seed(DBContext db)
        {
            db.Users.Add(new User { UsrId = 1, Email = "12345@mail.com", Password = "12345678sdklgfj", Username = "Svenan" });
            db.Games.Add(new Game { GameId = 730, GameName = "CS:GO", GameDiscription = "Counter-Strike: Global Offencive", GameLink = "https://store.steampowered.com/app/730/CounterStrike_Global_Offensive/" });
            db.Reviews.Add(new Review { GameId = 730, ReviewId = 1, ReviewScore = 10, ReviewText = "Лучшая игра, 10 из 10", UserId = 1 });
            base.Seed(db);
        }
    }
}