using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;

namespace ParserSite
{
    class ParserSites
    {
        public static string table { get; set; }
        /// <summary>
        /// Получить исходный код страницы
        /// </summary>
        /// <param name="link"> Url страницы </param>
        /// <returns></returns>
        public static string GetPage(string link)
        {
            HttpRequest request = new HttpRequest();
            string response = request.Get(link).ToString();
            return response;
        }
        public static string[] GetMatches(string page)
        {
            HtmlParser htmlParser = new HtmlParser();
            var parsePage = htmlParser.ParseDocument(page);
            //table = parsePage.QuerySelector("table.matches").OuterHtml;
            List<string> matches = new List<string>(); 
            foreach (var tmp in parsePage.QuerySelectorAll("div.event__match"))
            {
                matches.Add(tmp.GetAttribute("id").Split('_')[2]);
            }
            return matches.ToArray();
        }
        public static List<MatchState> GetMatch(string page)
        {
            HtmlParser htmlParser = new HtmlParser();
            var parsePage = htmlParser.ParseDocument(page);
            MatchState matchState = new MatchState();
            List<MatchState> matchStates = new List<MatchState>();
            List<string> score = new List<string>();

            matchState.Date = parsePage.QuerySelector("div.description__time").TextContent;
            matchState.CommandHome = parsePage.QuerySelector("div.team-text.tname-home>div.tname>div.tname__text>a.participant-imglink").TextContent;
            matchState.CommandAway = parsePage.QuerySelector("div.team-text.tname-away>div.tname>div.tname__text>a.participant-imglink").TextContent;
            matchState.MatchScoreHome = parsePage.QuerySelectorAll("span.scoreboard")[0].TextContent;
            matchState.MatchScoreAway = parsePage.QuerySelectorAll("span.scoreboard")[1].TextContent;
            //состав команд
            foreach (var HomePlayers in parsePage.QuerySelectorAll("td.fl>div.name>a"))
            {
                if (matchState.HomePlayers.Count > 17) break;
                matchState.HomePlayers.Add(HomePlayers.TextContent);
            }

            foreach (var AwayPlayers in parsePage.QuerySelectorAll("td.fr>div.name>a"))
            {
                if (matchState.AwayPlayers.Count > 17) break;
                matchState.AwayPlayers.Add(AwayPlayers.TextContent);
            }
            //Замены		

            string[] Replace = new string[4];
            //var tak = parsePage.QuerySelector("td.fl>div.icon-lineup>span.substitution-out");
            //var tmp = tak.Parent;
            //string str = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
            var i = 0;
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fl>div.icon-lineup>span.substitution-out"))
            {
                
                //if (matchState.HomePlayers.Count > 17) break;
                var tmp = HomeReplays.Parent;
                Replace[i] = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                ++i;
            }



            matchStates.Add(matchState);

            return matchStates;
        }
    }
}
