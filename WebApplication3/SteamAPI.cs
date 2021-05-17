using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication3
{
    public class UsersGamesInfo
    {
        readonly string GamesCount;
        readonly Dictionary<string, string> GamesIDnTime;
        public UsersGamesInfo(string count, Dictionary<string, string> idNtime)
        {
            GamesCount = count;
            GamesIDnTime = idNtime;
        }
        public string GetGamesCount()
        {
            return GamesCount;
        }
        public Dictionary<string,string> GetGamesIDnTime()
        {
            return GamesIDnTime;
        }
    }
    public class GameInfo
    {
        readonly string Name;
        readonly string Image;
        readonly string Description;
        public GameInfo(string name, string image, string descr)
        {
            Name = name;
            Image = image;
            Description = descr;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetImage()
        {
            return Image;
        }
        public string GetDescription()
        {
            return Description;
        }
    }
    public class SteamAPI
    {
        private static string GetInfo(string text, string start, string end)
        {
            var s = text.Remove(0, text.IndexOf(start) + start.Length);
            s = s.Remove(s.IndexOf(end));
            return s.Trim();
        }
        public static GameInfo GetGameInfo(string gameID)
        {
            var wc = new WebClient();
            var answer = wc.DownloadString("https://store.steampowered.com/app/"+gameID+"/");
            var name = GetInfo(answer, "apphub_AppName\">", "</div>");
            if (name.Length > 100)
                name = gameID;
            return new GameInfo(name, GetInfo(answer, "img class=\"game_header_image_full\" src=\"", "\">"),
                GetInfo(answer, "<div class=\"game_description_snippet\">", "</div>"));
        }
        public static UsersGamesInfo GetUsersGamesInfo(string userID)
        {
            var wc = new WebClient();
            var answer = wc.DownloadString("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=61115E80A2CDB0D150ACC0D51E2FCF4F&steamid=" + userID + "&format=xml");
            var text = answer.Remove(0, answer.IndexOf("<game_count>") + "<game_count>".Length);
            var count = text.Remove(text.IndexOf("</"));
            var gameStats = new Dictionary<string, string>();
            while (text.IndexOf("<appid>") != -1)
            {
                text = text.Remove(0, text.IndexOf("<appid>") + "<appid>".Length);
                var gameID = text.Remove(text.IndexOf("</appid>"));
                text = text.Remove(0, text.IndexOf("<playtime_forever>") + "<playtime_forever>".Length);
                var playTime = text.Remove(text.IndexOf("</playtime_forever>"));
                gameStats.Add(gameID, playTime);
            }
            return new UsersGamesInfo(count, gameStats);
        }
    }
}