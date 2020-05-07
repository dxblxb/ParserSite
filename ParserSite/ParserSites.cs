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
        public static MatchState GetMatch(string page)
        {
            HtmlParser htmlParser = new HtmlParser();
            var parsePage = htmlParser.ParseDocument(page);
            MatchState matchState = new MatchState();
            List<string> score = new List<string>();

            matchState.Date = parsePage.QuerySelector("div.description__time").TextContent;
            matchState.TrainerHome = parsePage.QuerySelector("table#coaches>tbody>tr.odd>td.fl").TextContent.Trim();
            matchState.TranerAway = parsePage.QuerySelector("table#coaches>tbody>tr.odd>td.fr").TextContent.Trim();
            matchState.CommandHome = parsePage.QuerySelector("div.team-text.tname-home>div.tname>div.tname__text>a.participant-imglink").TextContent;
            matchState.CommandAway = parsePage.QuerySelector("div.team-text.tname-away>div.tname>div.tname__text>a.participant-imglink").TextContent;
            matchState.MatchScoreHome = parsePage.QuerySelectorAll("span.scoreboard")[0].TextContent;
            matchState.MatchScoreAway = parsePage.QuerySelectorAll("span.scoreboard")[1].TextContent;

            //орбитр и стадион
            string[] matchInfo = new string[3];
            int infoCount = 0;
            foreach (var HomePlayers in parsePage.QuerySelectorAll("div.match-information-data>div.content"))
            {
                matchInfo[infoCount] =  HomePlayers.TextContent;
                ++infoCount;
            }
            matchState.Orbitr = matchInfo[0].Substring(matchInfo[0].IndexOf(":") + 1, (matchInfo[0].IndexOf(".") - matchInfo[0].IndexOf(":")));
            matchState.Stadion = matchInfo[2].Substring(matchInfo[2].IndexOf(":") + 1, (matchInfo[2].IndexOf("(") - matchInfo[2].IndexOf(":") - 2));

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

            #region Замены
            // замены дома
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fl>div.icon-lineup>span.substitution-out"))
            {
                //77' Вуд К. (Крауч П.)
                
                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string replace = title.Substring(0,index);
                string names = title.Substring(index + 2, title.Length - index - 2);
                int a=0, b=0;
                if (replace.IndexOf("+") > 0)
                {
                    index = replace.IndexOf("+");
                    a = int.Parse(replace.Substring(0, index));
                    b = int.Parse(replace.Substring(index+1, replace.Length-index-1));

                    replace = (a+b).ToString();
                }
                else
                {
                    a = int.Parse(replace);
                }
                if (a > 45) 
                {
                    continue;
                }else
                {
                    string[] Replace = new string[4];
                    Replace[0] = matchState.CommandHome;
                    Replace[1] = replace;
                    //"Мавропанос К. (Косьельни Л.)"
                    Replace[2] = (names.Substring(0, names.IndexOf("(") - 1));
                    Replace[3] = (names.Substring(names.IndexOf("(") + 1, (names.IndexOf(")") - names.IndexOf("(") - 1)));
                    matchState.ReplacePlayers.Add(Replace);
                }
                
            }

            //Замены	гости	
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fr>div.icon-lineup>span.substitution-out"))
            {
                //77' Вуд К. (Крауч П.)

                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string replace = title.Substring(0, index);
                string names = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (replace.IndexOf("+") > 0)
                {
                    index = replace.IndexOf("+");
                    a = int.Parse(replace.Substring(0, index));
                    b = int.Parse(replace.Substring(index + 1, replace.Length - index - 1));

                    replace = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(replace);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Replace = new string[4];
                    Replace[0] = matchState.CommandAway;
                    Replace[1] = replace;
                    //"Мавропанос К. (Косьельни Л.)"
                    Replace[2] = (names.Substring(0,names.IndexOf("(")-1));
                    Replace[3] = (names.Substring(names.IndexOf("(")+1,(names.IndexOf(")")- names.IndexOf("(")-1)));
                    matchState.ReplacePlayers.Add(Replace);
                }

            }
            #endregion

            #region желтые карты
            // желтые карты дома
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fl>div.icon-lineup>span.y-card"))
            {
                //77' Вуд К. (Крауч П.)

                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string card = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Card = new string[3];
                    Card[0] = matchState.CommandHome;
                    Card[1] = time;
                    Card[2] = card;
                    matchState.YCard.Add(Card);
                }

            }
            //желтые карты гость
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fr>div.icon-lineup>span.y-card"))
            {
                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string card = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Card = new string[3];
                    Card[0] = matchState.CommandAway;
                    Card[1] = time;
                    Card[2] = card;
                    matchState.YCard.Add(Card);
                }

            }
            #endregion

            #region красные карты
            // красные карты дома
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fl>div.icon-lineup>span.r-card"))
            {
                //77' Вуд К. (Крауч П.)

                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string card = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Card = new string[3];
                    Card[0] = matchState.CommandHome;
                    Card[1] = time;
                    Card[2] = card;
                    matchState.RCard.Add(Card);
                }

            }
            //красные карты гость
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fr>div.icon-lineup>span.r-card"))
            {
                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string card = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Card = new string[3];
                    Card[0] = matchState.CommandAway;
                    Card[1] = time;
                    Card[2] = card;
                    matchState.RCard.Add(Card);
                }

            }
            #endregion

            #region мячи
            // мячи дома
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fl>div.icon-lineup>span.soccer-ball"))
            {
                //77' Вуд К. (Крауч П.)

                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string names = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Ball = new string[4];
                    Ball[0] = matchState.CommandHome;
                    Ball[1] = time;
                    if (names.IndexOf("(") >= 0)
                    {
                       Ball[2] = (names.Substring(0, names.IndexOf("(") - 1));
                       Ball[3] = (names.Substring(names.IndexOf("(") + 1, (names.IndexOf(")") - names.IndexOf("(") - 1)));
                    }
                    else
                    {
                        Ball[2] = names.Trim(); 
                        Ball[3] = "";
                    }
                    
                    matchState.Ball.Add(Ball);
                    matchState.TotalFirstTimeHome = (int.Parse(matchState.TotalFirstTimeHome)+1).ToString();
                }

            }
            //мячи гость
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fr>div.icon-lineup>span.soccer-ball"))
            {
                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string names = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] Ball = new string[4];
                    Ball[0] = matchState.CommandHome;
                    Ball[1] = time;
                    if (names.IndexOf("(") >= 0)
                    {
                        Ball[2] = (names.Substring(0, names.IndexOf("(") - 1));
                        Ball[3] = (names.Substring(names.IndexOf("(") + 1, (names.IndexOf(")") - names.IndexOf("(") - 1)));
                    }
                    else
                    {
                        Ball[2] = names.Trim();
                        Ball[3] = "";
                    }
                    matchState.Ball.Add(Ball);
                    matchState.TotalFirstTimeAway = (int.Parse(matchState.TotalFirstTimeAway) + 1).ToString();
                }

            }

            #endregion

            #region автогол
            // автогол дома
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fl>div.icon-lineup>span.soccer-ball-own"))
            {
                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string names = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] AutoBall = new string[3];
                    AutoBall[0] = matchState.CommandHome;
                    AutoBall[1] = time;
                    AutoBall[2] = names.Trim();
                    matchState.Ball.Add(AutoBall);
                    matchState.TotalFirstTimeAway = (int.Parse(matchState.TotalFirstTimeAway) + 1).ToString();
                }

            }
            //автогол гость
            foreach (var HomeReplays in parsePage.QuerySelectorAll("td.fr>div.icon-lineup>span.soccer-ball-own"))
            {
                var tmp = HomeReplays.Parent;
                string title = (tmp as AngleSharp.Html.Dom.IHtmlDivElement).Title;
                int index = title.IndexOf("'");
                string time = title.Substring(0, index);
                string names = title.Substring(index + 2, title.Length - index - 2);
                int a = 0, b = 0;
                if (time.IndexOf("+") > 0)
                {
                    index = time.IndexOf("+");
                    a = int.Parse(time.Substring(0, index));
                    b = int.Parse(time.Substring(index + 1, time.Length - index - 1));

                    time = (a + b).ToString();
                }
                else
                {
                    a = int.Parse(time);
                }
                if (a > 45)
                {
                    continue;
                }
                else
                {
                    string[] AutoBall = new string[3];
                    AutoBall[0] = matchState.CommandHome;
                    AutoBall[1] = time;
                    AutoBall[2] = names.Trim();
                    matchState.Ball.Add(AutoBall);
                    matchState.TotalFirstTimeHome = (int.Parse(matchState.TotalFirstTimeHome) + 1).ToString();
                }

            }

            #endregion

            #region статистика
            foreach (var HomeReplays in parsePage.QuerySelectorAll("div#tab-statistics-1-statistic>div.statRow>div.statTextGroup"))
            {
                //37%Владение мячом63%
                string doc = HomeReplays.InnerHtml;
                HtmlParser htmlPar = new HtmlParser();
                var parseContent = htmlPar.ParseDocument(doc);
                string[] stats = new string[3];
                stats[0] = parseContent.QuerySelector("div.statText--homeValue").TextContent.Trim('%');
                stats[1] = parseContent.QuerySelector("div.statText--titleValue").TextContent;
                stats[2] = parseContent.QuerySelector("div.statText--awayValue").TextContent.Trim('%');
                matchState.Stats.Add(stats);
                

            }
            #endregion

            return matchState;
        }
    }
}
