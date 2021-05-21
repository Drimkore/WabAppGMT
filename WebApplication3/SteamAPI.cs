using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace testapp2
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
        public Dictionary<string, string> GetGamesIDnTime()
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
            HtmlWeb web = new HtmlWeb() {UseCookies = true };
            var htmlDoc = web.Load("https://store.steampowered.com/app/" + gameID + "/?l=russian");

            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='apphub_AppName']");
            if(node == null)
            {
                return new GameInfo("NTE", "", "");
            }
            var name = node.InnerText.Trim();
            node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='game_description_snippet']");
            var descr = node.InnerText.Trim();
            node = htmlDoc.DocumentNode.SelectSingleNode("//img[@class='game_header_image_full']");
            var img = node.Attributes["src"].Value.Trim();
            return new GameInfo(name, img, descr);
        }
        public static UsersGamesInfo GetUsersGamesInfo(string userID)
        {
            var gameStats = new Dictionary<string, string>();
            var count = "";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=61115E80A2CDB0D150ACC0D51E2FCF4F&steamid=76561198091848209&format=xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "#text")
                    {
                        count = childnode.InnerText;
                    }
                    if (childnode.Name == "message")
                    {
                        var id = "";
                        var time = "";
                        foreach (XmlNode childnode2 in childnode.ChildNodes)
                        { 
                            if (childnode2.Name == "appid")
                            {
                                id = childnode2.InnerText;
                            }
                            if (childnode2.Name == "playtime_forever")
                            {
                                time = childnode2.InnerText;
                            }
                            if (id != "" && time != "")
                            {
                                gameStats.Add(id, time);
                                id = "";
                                time = "";
                            }
                        }
                    }
                }
            }
            return new UsersGamesInfo(count, gameStats);
        }
    }
}
