using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;
using Quartz;
using System.Threading.Tasks;

namespace WebApplication3.Jobs
{
    public class GameAdder : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var gamer = SteamAPI.GetUsersGamesInfo("76561197990283731");
            using (DBContext db = new DBContext())
            {
                foreach (var i in gamer.GetGamesIDnTime())
                {
                    if (db.Games.Any(u => u.SteamId == i.Key))
                        continue;
                    var game = SteamAPI.GetGameInfo(i.Key);
                    if (game.GetName() == "NTE")
                        continue;
                    db.Games.Add(new Game
                    {
                        SteamId = i.Key,
                        GameName = game.GetName(),
                        GameDiscription = game.GetDescription(),
                        GameLink = "https://store.steampowered.com/app/" + i.Key
                    });
                }
                db.SaveChanges();
            }




        }
    }
}